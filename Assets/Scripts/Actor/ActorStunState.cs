using UnityEngine;

namespace RitualRhythm.Actor {
	public class ActorStunState : IActorState {
		private float StunDuration;
		
		private float _timeElapsed;

		public ActorStunState(float stunTime) {
			StunDuration = stunTime;
		}
		
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
			return this;
		}
		
		public IActorState GetHurtBadly(ActorModel actorModel) {
			return this;
		}
		
		public IActorState Update(ActorModel actorModel, float deltaTime) {
			_timeElapsed += deltaTime;
			if (_timeElapsed < StunDuration) {
				return this;
			} else {
				actorModel.AnimationState = ActorAnimationState.Walking;
				return new ActorWalkState();
			}
		}
	}
}