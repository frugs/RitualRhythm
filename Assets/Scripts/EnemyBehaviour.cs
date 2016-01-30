using System.Collections;
using UnityEngine;

namespace RitualRhythm {
    public class EnemyBehaviour : MonoBehaviour {

		public Sound soundManager;

        private const float HurtAnimationLength = 0.05f;

        private SpriteRenderer _spriteRenderer;
        
        private bool _hurt;
        private IEnumerator _hurtAnimRoutine = EnumeratorUtils.EmptyEnumerator();

        public void Start () {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
	
        public void Update () {
//			if (soundManager.isOnBeat (out offset)) {
//				_hurtAnimRoutine = PlayHurtAnimation();
//				soundManager.playPunch();
//			} 
            if (_hurt) {
                _hurtAnimRoutine = PlayHurtAnimation();
                _hurt = false;
            }
            _hurtAnimRoutine.MoveNext();
        }

        public void OnTriggerEnter(Collider col) {
            _hurt = true;
        }

        private IEnumerator PlayHurtAnimation() {
            var t = 0f;
            var originalColor = _spriteRenderer.color;
            while (t < HurtAnimationLength / 2) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(originalColor, Color.red, t);
                yield return null;
            }
            _spriteRenderer.color = Color.red;

            while (t < HurtAnimationLength) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(Color.red, originalColor, t);
                yield return null;
            }
            _spriteRenderer.color = originalColor;
        }
    }
}
