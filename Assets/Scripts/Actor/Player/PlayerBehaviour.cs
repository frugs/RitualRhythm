using System;
using UnityEngine;

namespace RitualRhythm.Actor.Player {
    public class PlayerBehaviour : ActorBehaviour {

		public Sound soundManager;
		private const float PlayerVelocity = 4f;

		private int health = 100;

		private bool _hurt;

        private readonly ResponsiveButtonDirectionalInput _directionalInput =
            new ResponsiveButtonDirectionalInput(
                InputUtil.Up,
                InputUtil.Down,
                InputUtil.Left,
                InputUtil.Right);

        public BeatState beatState;

        protected override Rigidbody Rigidbody {
            get { return GetComponent<Rigidbody>(); }
        }

        public ActorModel ActorModel { get; private set; }

        public virtual void Start() {
            ActorModel = new ActorModel(transform.position);
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
                }  else {
                    Debug.Log("Not allowed");
                    ActorModel.GetHurt();
                }
            }

			handleHurt();

            ActorModel.Update(Time.deltaTime);

            var animator = GetComponent<Animator>();

            animator.SetFloat(
                "Speed", 
                Mathf.Abs(_directionalInput.Horizontal) + Mathf.Abs(_directionalInput.Vertical));
        }

		void handleHurt ()
		{
			if (_hurt) {
				health -= 25;
				_hurt = false;
				if (health <= 0) {
					Debug.Log ("Lose");
					soundManager.playDeath();
					ActorModel.GetHurtBadly ();
					health = 100;
					return;
				}
				Debug.Log ("Ow");
				ActorModel.GetHurt ();
				soundManager.playPain();
			}

		}

		public void OnTriggerEnter(Collider col) {
			_hurt = true;
		}

        public override void PositionUpdated(Vector2 position) {
            UpdatePosition(position);
        }

        public override void LookDirectionUpdated(Vector2 lookDirection) {
            UpdateLookDirection(lookDirection);
        }

        public override void AnimationStateUpdated(ActorAnimationState state) {
            UpdateAnimationState(state);
            if (state == ActorAnimationState.Attacking) {
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
}
