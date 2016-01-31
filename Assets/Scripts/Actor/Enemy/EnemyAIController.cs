using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RitualRhythm.Actor.Enemy {
    public class EnemyAiController {

        private enum AiType {
            Attack,
            Idle,
            WanderAround
        }

        private readonly ActorModel _actorModel;
        private readonly float _attackRange;
        private readonly float _velocity;

        private AiType _aiType;
        private Vector2 _wanderDirection;
        private double _decisionTimer;
		private Sound soundManager;

		public string character;

        public EnemyAiController(ActorModel actorModel, float attackRange, float velocity, Sound soundManager) {
            this._actorModel = actorModel;
            _attackRange = attackRange;
            _velocity = velocity;
			this.soundManager = soundManager;

            PickAiType();
            ResetDecisionTimer();
        }

        private void PickAiType() {
			var rand = Random.value;
			if (rand < 0.2) {
				character = Catalogue.Character.A;
			} else if (rand < 0.4) {
				character = Catalogue.Character.H;
			} else if (rand < 0.6) {
				character = Catalogue.Character.D;
			} else if (rand < 0.8) {
				character = Catalogue.Character.M;
			} else if (rand < 1.0) {
				character = Catalogue.Character.O;
			}
            var values = Enum.GetValues(typeof(AiType));
            _aiType = (AiType)values.GetValue(Random.Range(0, values.Length));
            _wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        }

        public void Update(float deltaTime, Vector2 playerPosition) {
            _decisionTimer -= deltaTime;
            if (_decisionTimer < 0f) {
                ResetDecisionTimer();
                PickAiType();
            }

            var playerDisplacement = playerPosition - _actorModel.Position;
            var playerDirection = playerDisplacement.normalized;
            _actorModel.LookTowards(playerDirection.x > 0 ? Vector2.right : Vector2.left);

            if (_aiType == AiType.Attack) {
                if (playerDisplacement.magnitude >_attackRange) {
                    var moveBy = playerDirection * _velocity * deltaTime;
                    _actorModel.MoveBy(moveBy);
                } else if (Random.value > 0.7f) {
                    _actorModel.Attack();
					soundManager.playVox(character, Catalogue.Type.STRIKE);
                }
            }

            if (_aiType == AiType.WanderAround) {
                _actorModel.MoveBy(_wanderDirection * _velocity * deltaTime);
            }
        }

        private void ResetDecisionTimer() {
            _decisionTimer = Random.Range(3f, 6f);
        }
    }
}
