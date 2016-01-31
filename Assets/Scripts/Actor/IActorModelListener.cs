using UnityEngine;

namespace RitualRhythm.Actor {
    public interface IActorModelListener {
        void PositionUpdated(Vector2 position);

        void LookDirectionUpdated(Vector2 lookDirection);

        void AnimationStateUpdated(ActorAnimationState state);
    }
}