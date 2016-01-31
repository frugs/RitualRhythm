using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorAttackState : BaseActorState {
        
        private const float AttackDuration = 0.3f;

        private float _timeElapsed;

        public override IActorState Jump(ActorModel actorModel) {
            return this;
        }

        public override IActorState LookTowards(ActorModel actorModel, Vector2 direction) {
            return this;
        }

        public override IActorState Move(ActorModel actorModel, Vector2 position) {
            return this;
        }

        public override IActorState Attack(ActorModel actorModel) {
            return this;
        }

        public override IActorState Update(ActorModel actorModel, float deltaTime) {
            _timeElapsed += deltaTime;
            if (_timeElapsed < AttackDuration) {
                return this;
            } else {
                actorModel.AnimationState = ActorAnimationState.Walking;
                return new ActorWalkState();
            }
        }
    }
}