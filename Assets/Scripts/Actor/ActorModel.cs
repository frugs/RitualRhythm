using System.Collections.Generic;
using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorModel {

        private IActorState _actorState = new ActorWalkState();

        private readonly IList<IActorModelListener> _listeners =
            new List<IActorModelListener>();

        private Vector2 _position;
        private Vector2 _lookDirection = Vector2.right;

        private bool _isAttacking;

        public Vector2 Position {
            get { return _position; }
            set {
                _position = value;
                foreach (var listener in _listeners) {
                    listener.PositionUpdated(value);
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

        public bool IsAttacking {
            get { return _isAttacking; }
            set {
                _isAttacking = value;
                foreach (var listener in _listeners) {
                    listener.AttackStateUpdated(value);
                }
            }
        }

        public ActorModel(Vector2 position) {
            _position = position;
        }

        public void Jump() {
            _actorState = _actorState.Jump(this);
        }

        public void LookTowards(Vector2 direction) {
            _actorState = _actorState.LookTowards(this, direction);
        }

        public void MoveBy(Vector2 direction) {
            _actorState = _actorState.Move(this, direction);
        }

        public void Attack() {
            _actorState = _actorState.Attack(this);
        }

        public void GetHurt() {
            _actorState = _actorState.GetHurt(this);
        }

        public void GetHurtBadly() {
            _actorState = _actorState.GetHurtBadly(this);
        }

        public void Update(float deltaTime) {
            _actorState = _actorState.Update(this, deltaTime);
        }

        public void RegisterListener(IActorModelListener listener) {
            _listeners.Add(listener);
        }
    }
}
