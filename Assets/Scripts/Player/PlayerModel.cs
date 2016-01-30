using System.Collections.Generic;
using UnityEngine;

namespace RitualRhythm.Player {
    public class PlayerModel {
        private IPlayerState _playerState = new PlayerWalkState();

        private readonly IList<IPlayerModelListener> _listeners =
            new List<IPlayerModelListener>();

        private Vector2 _position;
        private Vector2 _lookDirection = Vector2.right;

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

        public PlayerModel(Vector2 position) {
            _position = position;
        }

        public void Jump() {
            _playerState = _playerState.Jump(this);
        }

        public void LookTowards(Vector2 direction) {
            _playerState = _playerState.LookTowards(this, direction);
        }

        public void MoveBy(Vector2 direction) {
            _playerState = _playerState.Move(this, direction);
        }

        public void Attack() {
            _playerState = _playerState.Attack(this);
        }

        public void GetHurt() {
            _playerState = _playerState.GetHurt(this);
        }

        public void GetHurtBadly() {
            _playerState = _playerState.GetHurtBadly(this);
        }

        public void RegisterListener(IPlayerModelListener listener) {
            _listeners.Add(listener);
        }
    }
}
