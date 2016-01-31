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

            base.Update();

            var animator = GetComponent<Animator>();

            animator.SetFloat(
                "Speed", 
                Mathf.Abs(_directionalInput.Horizontal) + Mathf.Abs(_directionalInput.Vertical));
        }

        public override void AnimationStateUpdated(ActorAnimationState state) {
            if (state == ActorAnimationState.Attacking) {
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
}
