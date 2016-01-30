using UnityEngine;

namespace RitualRhythm.Player {
    public class PlayerWalkState : IPlayerState {
        public IPlayerState Jump(PlayerModel playerModel) {
            throw new System.NotImplementedException();
        }

        public IPlayerState LookTowards(PlayerModel playerModel, Vector2 direction) {
            playerModel.LookDirection = direction;
            return this;
        }

        public IPlayerState Move(PlayerModel playerModel, Vector2 position) {
            playerModel.Position += position;
            return this;
        }

        public IPlayerState Attack(PlayerModel playerModel) {
            throw new System.NotImplementedException();
        }

        public IPlayerState GetHurt(PlayerModel playerModel) {
            throw new System.NotImplementedException();
        }

        public IPlayerState GetHurtBadly(PlayerModel playerModel) {
            throw new System.NotImplementedException();
        }
    }
}
