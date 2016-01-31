using System;
using UnityEngine;

namespace RitualRhythm.Actor {
    public abstract class ActorBehaviour : MonoBehaviour, IActorModelListener {

        protected abstract Rigidbody Rigidbody { get; }

        protected void UpdatePosition(Vector2 position) {
            Rigidbody.MovePosition(new Vector3(position.x, position.y, position.y));
        }

        protected void UpdateLookDirection(Vector2 lookDirection) {
            if (lookDirection.Equals(Vector2.left)) {
                transform.localScale = new Vector3(
                        -Mathf.Abs(transform.localScale.x),
                        transform.localScale.y,
                        transform.localScale.z);
            } else {
                transform.localScale = new Vector3(
                        Mathf.Abs(transform.localScale.x),
                        transform.localScale.y,
                        transform.localScale.z);
            }
        }

        protected void UpdateAnimationState(ActorAnimationState state) {
            transform.Find("Arm").gameObject.SetActive(state == ActorAnimationState.Attacking);

            if (state == ActorAnimationState.Death) {
                Destroy(gameObject);
            }
        }
        
        public abstract void PositionUpdated(Vector2 position);
        public abstract void LookDirectionUpdated(Vector2 lookDirection);
        public abstract void AnimationStateUpdated(ActorAnimationState state);
    }
}
