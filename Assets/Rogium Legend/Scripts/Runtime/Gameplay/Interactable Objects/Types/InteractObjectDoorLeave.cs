using System;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Entities.Player;
using UnityEngine;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// Allows the player to move on to the next room upon touching it.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractObjectDoorLeave : MonoBehaviour, IInteractObject
    {
        private Vector2 direction;
        
        public void Construct(ObjectAsset data, ParameterInfo parameters)
        {
            DirectionType dir = (DirectionType) parameters.intValue1;
            direction = dir switch
            {
                DirectionType.Up => Vector2.up,
                DirectionType.Down => Vector2.down,
                DirectionType.Right => Vector2.right,
                DirectionType.Left => Vector2.left,
                _ => throw new ArgumentOutOfRangeException($"Direction of type '{dir}' is not supported.")
            };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController player))
            {
                if (player.ActionsLocked) return;
                GameplayOverseer.Instance.LoadNextNormalRoom();
                player.ForceMove(direction, 1, 0.2f);
            }
        }
    }
}