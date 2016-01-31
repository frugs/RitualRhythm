using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorWalkState : IActorState {
        public IActorState Jump(ActorModel actorModel) {
            throw new System.NotImplementedException();
        }

        public IActorState LookTowards(ActorModel actorModel, Vector2 direction) {
            actorModel.LookDirection = direction;
            return this;
        }

        public IActorState Move(ActorModel actorModel, Vector2 position) {
            actorModel.Position += position;
            return this;
        }

        public IActorState Attack(ActorModel actorModel) {
            actorModel.AnimationState = ActorAnimationState.Attacking;
            return new ActorAttackState();
        }

        public IActorState GetHurt(ActorModel actorModel) {
			actorModel.Position -= actorModel.LookDirection * 0.8f;
			return new ActorStunState(0.7f);
        }

        public IActorState GetHurtBadly(ActorModel actorModel) {
            actorModel.Position -= actorModel.LookDirection * 1.5f;
			return new ActorStunState(2f);
        }

        public IActorState Update(ActorModel actorModel, float deltaTime) {
            return this;
        }
    }
}
