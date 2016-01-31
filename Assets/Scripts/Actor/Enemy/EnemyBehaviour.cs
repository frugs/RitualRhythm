using System.Collections;
using RitualRhythm;
using RitualRhythm.Actor;
using RitualRhythm.Actor.Player;
using UnityEngine;

namespace RitualRhythm.Actor.Enemy {
    public class EnemyBehaviour : ActorBehaviour {
        private const float HurtAnimationLength = 0.25f;

        private SpriteRenderer _spriteRenderer;
        private bool _hurt;
        private IEnumerator _hurtAnimRoutine = EnumeratorUtils.EmptyEnumerator();
        private Color _originalColor;
        private ActorModel _enemyModel;
        private EnemyAiController _aiController;

        public PlayerBehaviour PlayerBehaviour;
        public BeatState BeatState;
        public BeatExecutor BeatExecutor;
        public Sound soundManager;
        public float InitialHealth;

        protected override Rigidbody Rigidbody {
            get { return GetComponent<Rigidbody>();  }
        }

        public void Start () {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            _enemyModel = new ActorModel(transform.position, InitialHealth);
            _enemyModel.RegisterListener(this);

            _aiController = new EnemyAiController(_enemyModel, 1f, 3f, soundManager);
        }
	
        public void Update () {
            if (_hurt) {
                _hurtAnimRoutine = PlayHurtAnimation();
				soundManager.playVox(Catalogue.Character.O, Catalogue.Type.HURT);
                _hurt = false;
            }
            _hurtAnimRoutine.MoveNext();

            _aiController.Update(Time.deltaTime, PlayerBehaviour.ActorModel.Position);
            
            _enemyModel.Update(Time.deltaTime);
        }

        public void OnTriggerEnter(Collider col) {
            _enemyModel.GetHurt();

            _hurt = true;
        }

        private IEnumerator PlayHurtAnimation() {
            var t = 0f;
            while (t < HurtAnimationLength / 2) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(_originalColor, Color.red, t);
                yield return null;
            }
            _spriteRenderer.color = Color.red;

            while (t < HurtAnimationLength) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(Color.red, _originalColor, t);
                yield return null;
            }
            _spriteRenderer.color = _originalColor;
        }

        public override void PositionUpdated(Vector2 position) {
            UpdatePosition(position);
        }

        public override void LookDirectionUpdated(Vector2 lookDirection) {
            UpdateLookDirection(lookDirection);
        }

        public override void AnimationStateUpdated(ActorAnimationState state) {

            UpdateAnimationState(state);
            if (state == ActorAnimationState.GettingHurt)
            {
                soundManager.playImpact(Catalogue.Type.BIG);
            }
            if (state == ActorAnimationState.Death)
            {
                soundManager.playImpact(Catalogue.Type.BIG);
				soundManager.playVox (_aiController.character, Catalogue.Type.DEATH);
			}
        }
    }
}
