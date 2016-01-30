using System;
using UnityEngine;

namespace RitualRhythm.Player {
    public class PlayerBehaviour : MonoBehaviour, IPlayerModelListener {

        private const float PlayerVelocity = 4f;

        private readonly ResponsiveButtonDirectionalInput _directionalInput =
            new ResponsiveButtonDirectionalInput(
                InputUtil.Up,
                InputUtil.Down,
                InputUtil.Left,
                InputUtil.Right);

        private PlayerModel _playerModel;

        private Rigidbody _rigidBody;

        public void Start() {
            _rigidBody = GetComponent<Rigidbody>();
            _playerModel = new PlayerModel(transform.position);
            _playerModel.RegisterListener(this);
        }
	
        public void Update() {
            _directionalInput.Update();

            if (Math.Abs(_directionalInput.Horizontal) > 0.5f) {
                _playerModel.LookTowards(new Vector2(_directionalInput.Horizontal, 0));
            }
            var moveAmount = new Vector2(
                _directionalInput.Horizontal * PlayerVelocity * Time.smoothDeltaTime,
                _directionalInput.Vertical * PlayerVelocity * Time.smoothDeltaTime);
            _playerModel.MoveBy(moveAmount);
        }

        public void PositionUpdated(Vector2 position) {
            _rigidBody.MovePosition(position);
        }

        public void LookDirectionUpdated(Vector2 lookDirection) {
            if (lookDirection.Equals(Vector2.left)) {
                transform.localScale = new Vector3(
                        -Mathf.Abs(transform.localScale.x),
                        transform.localScale.y,
                        transform.localScale.z);
            } else {
                transform.localScale = new Vector3(
                        Mathf.Abs(transform.localScale.x),
                        transform.localScale.y,
                        transform.localScale.z);
            }
        }
    }
}
