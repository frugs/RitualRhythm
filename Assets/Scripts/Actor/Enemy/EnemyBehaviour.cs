using System.Collections;
using UnityEngine;

namespace RitualRhythm.Actor.Enemy {
    public class EnemyBehaviour : MonoBehaviour {

        private const float HurtAnimationLength = 0.5f;

        private SpriteRenderer _spriteRenderer;
        
        private bool _hurt;
        private IEnumerator _hurtAnimRoutine = EnumeratorUtils.EmptyEnumerator();
        private Color _originalColor;

        public void Start () {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
        }
	
        public void Update () {
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
            while (t < HurtAnimationLength / 2) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(_originalColor, Color.red, t);
                yield return null;
            }
            _spriteRenderer.color = Color.red;

            while (t < HurtAnimationLength) {
                t += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(Color.red, _originalColor, t);
                yield return null;
            }
            _spriteRenderer.color = _originalColor;
        }
    }
}
