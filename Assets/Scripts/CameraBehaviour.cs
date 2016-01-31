using System.Collections;
using UnityEngine;

namespace RitualRhythm {
    public class CameraBehaviour : MonoBehaviour {

        public GameObject FollowTarget;
        public GameObject Boss;
        public Sound Sound;

        private float _minX;

        public void Update () {
            _minX = Mathf.Max(_minX, FollowTarget.transform.position.x);
            transform.position = new Vector3(_minX, transform.position.y, transform.position.z);

            if (Mathf.Abs(Boss.transform.position.x - transform.position.x) < 6f) {
                Sound.setState(2);
            }
        }
    }
}
