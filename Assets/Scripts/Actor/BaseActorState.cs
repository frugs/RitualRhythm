using UnityEngine;

namespace RitualRhythm.Actor {
    public abstract class BaseActorState : IActorState {
        public abstract IActorState Jump(ActorModel actorModel);
        public abstract IActorState LookTowards(ActorModel actorModel, Vector2 direction);
        public abstract IActorState Move(ActorModel actorModel, Vector2 position);
        public abstract IActorState Attack(ActorModel actorModel);

        public IActorState GetHurt(ActorModel actorModel) {
            actorModel.Position -= actorModel.LookDirection * 3f;
            actorModel.Health -= 25f;
            if (actorModel.Health > 5) {
                actorModel.AnimationState = ActorAnimationState.GettingHurt;
                return new ActorStunState(0.2f);
            } else {
                actorModel.AnimationState = ActorAnimationState.Death;
                return GetHurtBadly(actorModel);
            }
        }

        public IActorState GetHurtBadly(ActorModel actorModel) {
            return this;
        }

        public abstract IActorState Update(ActorModel actorModel, float deltaTime);
    }
}