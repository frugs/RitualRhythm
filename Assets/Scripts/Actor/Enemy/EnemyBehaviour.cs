using System.Collections;
using RitualRhythm;
using RitualRhythm.Actor;
using RitualRhythm.Actor.Player;
using UnityEngine;

namespace RitualRhythm.Actor.Enemy {
    public class EnemyBehaviour : ActorBehaviour {
        private const float HurtAnimationLength = 0.25f;

        private EnemyAiController AiController;

        private SpriteRenderer _spriteRenderer;
		public Sound soundManager;
        
        private bool _hurt;
        private IEnumerator _hurtAnimRoutine = EnumeratorUtils.EmptyEnumerator();
        private Color _originalColor;

        public PlayerBehaviour PlayerBehaviour;

        public override void Start () {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            base.Start();

            AiController = new EnemyAiController(ActorModel, 1f, 3f);
        }
	
        public override void Update () {
            if (_hurt) {
                _hurtAnimRoutine = PlayHurtAnimation();
				soundManager.playPain();
                _hurt = false;
            }
            _hurtAnimRoutine.MoveNext();

            AiController.Update(Time.deltaTime, PlayerBehaviour.ActorModel.Position);

            base.Update();
        }

        public void OnTriggerEnter(Collider col) {
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
    }
}
