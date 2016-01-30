using System;
using UnityEngine;

namespace RitualRhythm.Actor {
    public class ActorBehaviour : MonoBehaviour, IActorModelListener {

        protected ActorModel ActorModel { get; private set; }
        protected Rigidbody Rigidbody { get; private set; }

        public virtual void Start() {
            Rigidbody = GetComponent<Rigidbody>();
            ActorModel = new ActorModel(transform.position);
            ActorModel.RegisterListener(this);
        }

        public virtual void Update() {
            ActorModel.Update(Time.deltaTime);
        }
        
        public void PositionUpdated(Vector2 position) {
            Rigidbody.MovePosition(new Vector3(position.x, position.y, position.y));
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

        public void AttackStateUpdated(bool isAttacking) {
            transform.Find("Arm").gameObject.SetActive(isAttacking);
        }
    }
}
