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

        public override void Update() {
            _directionalInput.Update();

            if (Mathf.Abs(_directionalInput.Horizontal) > 0) {
                ActorModel.LookTowards(new Vector2(_directionalInput.Horizontal, 0));
            }

            ActorModel.MoveBy(new Vector2(
                _directionalInput.Horizontal * PlayerVelocity * Time.deltaTime,
                _directionalInput.Vertical * PlayerVelocity * Time.deltaTime));

            if (Input.GetButtonDown("Fire1")) {
                ActorModel.Attack();
            }

			handleHurt();

            base.Update();

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

        public override void AnimationStateUpdated(ActorAnimationState state) {
            if (state == ActorAnimationState.Attacking) {
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
}
