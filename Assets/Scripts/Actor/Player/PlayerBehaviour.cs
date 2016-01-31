using System;
using UnityEngine;

namespace RitualRhythm.Actor.Player {
    public class PlayerBehaviour : ActorBehaviour {

		private const float PlayerVelocity = 4f;

        private readonly ResponsiveButtonDirectionalInput _directionalInput =
            new ResponsiveButtonDirectionalInput(
                InputUtil.Up,
                InputUtil.Down,
                InputUtil.Left,
                InputUtil.Right);

        public Sound soundManager;
        public BeatState beatState;

        protected override Rigidbody Rigidbody {
            get { return GetComponent<Rigidbody>(); }
        }

        public ActorModel ActorModel { get; private set; }

        public virtual void Start() {
            ActorModel = new ActorModel(transform.position, 100f);
            ActorModel.RegisterListener(this);
        }

        public void Update() {
            _directionalInput.Update();

            if (Mathf.Abs(_directionalInput.Horizontal) > 0) {
                ActorModel.LookTowards(new Vector2(_directionalInput.Horizontal, 0));
            }

            ActorModel.MoveBy(new Vector2(
                _directionalInput.Horizontal * PlayerVelocity * Time.deltaTime,
                _directionalInput.Vertical * PlayerVelocity * Time.deltaTime));

            if (Input.GetButtonDown("Fire1")) {
                if (beatState.isInBeat()) {
                    ActorModel.Attack();
					soundManager.playImpact(Catalogue.Type.MISS);
                }  else {
                    Debug.Log("Not allowed");
                    ActorModel.GetHurt();
                }
            }

            ActorModel.Update(Time.deltaTime);

            var animator = GetComponent<Animator>();

            animator.SetFloat(
                "Speed", 
                Mathf.Abs(_directionalInput.Horizontal) + Mathf.Abs(_directionalInput.Vertical));
        }
        
		public void OnTriggerEnter(Collider col) {
			ActorModel.GetHurt();
		}

        public override void PositionUpdated(Vector2 position) {
            UpdatePosition(position);
        }

        public override void LookDirectionUpdated(Vector2 lookDirection) {
            UpdateLookDirection(lookDirection);
        }

        public override void AnimationStateUpdated(ActorAnimationState state) {
            if (state == ActorAnimationState.GettingHurt) {
                Debug.Log("Ow");
                soundManager.playPain();
            }

            if (state == ActorAnimationState.Death) {
                Debug.Log("Lose");
                soundManager.playDeath();
            }

            UpdateAnimationState(state);
        }
    }
}
