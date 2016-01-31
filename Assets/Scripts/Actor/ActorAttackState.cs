using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorAttackState : IActorState {
        private const float AttackDuration = 0.3f;

        private float _timeElapsed;

        public IActorState Jump(ActorModel actorModel) {
            return this;
        }

        public IActorState LookTowards(ActorModel actorModel, Vector2 direction) {
            return this;
        }

        public IActorState Move(ActorModel actorModel, Vector2 position) {
            return this;
        }

        public IActorState Attack(ActorModel actorModel) {
            return this;
        }

        public IActorState GetHurt(ActorModel actorModel) {
            throw new System.NotImplementedException();
        }

        public IActorState GetHurtBadly(ActorModel actorModel) {
            throw new System.NotImplementedException();
        }

        public IActorState Update(ActorModel actorModel, float deltaTime) {
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