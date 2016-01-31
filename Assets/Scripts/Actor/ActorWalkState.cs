using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorWalkState : BaseActorState {
        public override IActorState Jump(ActorModel actorModel) {
            throw new System.NotImplementedException();
        }

        public override IActorState LookTowards(ActorModel actorModel, Vector2 direction) {
            actorModel.LookDirection = direction;
            return this;
        }

        public override IActorState Move(ActorModel actorModel, Vector2 position) {
            actorModel.Position += position;
            return this;
        }

        public override IActorState Attack(ActorModel actorModel) {
            actorModel.AnimationState = ActorAnimationState.Attacking;
            return new ActorAttackState();
        }

        public override IActorState Update(ActorModel actorModel, float deltaTime) {
            return this;
        }
    }
}
