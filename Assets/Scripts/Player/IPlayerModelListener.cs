using RitualRhythm.Player;
using UnityEngine;

namespace RitualRhythm {
    public interface IPlayerModelListener {
        void PositionUpdated(Vector2 position);

        void LookDirectionUpdated(Vector2 lookDirection);
    }
}