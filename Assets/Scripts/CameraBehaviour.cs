using UnityEngine;

namespace RitualRhythm {
    public class CameraBehaviour : MonoBehaviour {

        public GameObject FollowTarget;

        private float _minX;

        public void Update () {
            _minX = Mathf.Max(_minX, FollowTarget.transform.position.x);
            transform.position = new Vector3(_minX, transform.position.y, transform.position.z);
        }
    }
}
