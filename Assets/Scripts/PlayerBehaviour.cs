using UnityEngine;

namespace RitualRhythm {
    public class PlayerBehaviour : MonoBehaviour {

        private const float PlayerVelocity = 3f;

        private readonly ResponsiveButtonDirectionalInput _directionalInput =
            new ResponsiveButtonDirectionalInput(
                InputUtil.Up,
                InputUtil.Down,
                InputUtil.Left,
                InputUtil.Right);

        private Rigidbody _rigidBody;

        public void Start() {
            _rigidBody = GetComponent<Rigidbody>();
        }
	
        public void Update() {
            _directionalInput.Update();

            _rigidBody.velocity = Vector3.zero;

            _rigidBody.velocity += new Vector3(
                PlayerVelocity * _directionalInput.GetHorizontal(),
                PlayerVelocity * _directionalInput.GetVertical());
        }
    }
}
