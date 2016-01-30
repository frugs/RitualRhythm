using UnityEngine;

namespace RitualRhythm.Player {
    public interface IPlayerState {
        IPlayerState Jump(PlayerModel playerModel);
        IPlayerState LookTowards(PlayerModel playerModel, Vector2 direction);
        IPlayerState Move(PlayerModel playerModel, Vector2 position);
        IPlayerState Attack(PlayerModel playerModel);
        IPlayerState GetHurt(PlayerModel playerModel);
        IPlayerState GetHurtBadly(PlayerModel playerModel);
    }
}