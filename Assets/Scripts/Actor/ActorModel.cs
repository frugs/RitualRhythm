using System.Collections.Generic;
using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorModel {

        private IActorState _actorState = new ActorWalkState();

        private readonly IList<IActorModelListener> _listeners =
            new List<IActorModelListener>();

		private BeatState _beatState;
        private Vector2 _position;
        private Vector2 _lookDirection = Vector2.right;

        private ActorAnimationState _animationState;

        private const float MinY = -5.5f;
        private const float MaxY = -2f;

        public Vector2 Position {
            get { return _position; }
            set {
                var newPosition = ClampPositionInWalkableArea(value);

                _position = newPosition;
                foreach (var listener in _listeners) {
                    listener.PositionUpdated(newPosition);
                }
            }
        }

        public Vector2 LookDirection {
            get { return _lookDirection; }
            set {
                _lookDirection = value;
                foreach (var listener in _listeners) {
                    listener.LookDirectionUpdated(value);
                }
            }
        }

        public ActorAnimationState AnimationState {
            get { return _animationState; }
            set {
                _animationState = value;
                foreach (var listener in _listeners) {
                    listener.AnimationStateUpdated(_animationState);
                }
            }
        }

        public ActorModel(Vector2 position, BeatState beatState) {
            _position = position;
			_beatState = beatState;
        }

        public virtual void Jump() {
            _actorState = _actorState.Jump(this);
        }

        public virtual void LookTowards(Vector2 direction) {
            _actorState = _actorState.LookTowards(this, direction);
        }

        public virtual void MoveBy(Vector2 direction) {
            _actorState = _actorState.Move(this, direction);
        }

        public virtual void Attack() {
			if (_beatState.isInBeat ()) {
				_actorState = _actorState.Attack (this);
			} else {
				Debug.Log("Not allowed");
				GetHurt();
			}
        }

        public virtual void GetHurt() {
            _actorState = _actorState.GetHurt(this);
        }

        public virtual void GetHurtBadly() {
            _actorState = _actorState.GetHurtBadly(this);
        }

        public virtual void Update(float deltaTime) {
            _actorState = _actorState.Update(this, deltaTime);
        }

        public void RegisterListener(IActorModelListener listener) {
            _listeners.Add(listener);
        }

        private Vector2 ClampPositionInWalkableArea(Vector2 position) {
            var y = Mathf.Clamp(position.y, MinY, MaxY);
            return new Vector2(position.x, y);
        }
    }
}
