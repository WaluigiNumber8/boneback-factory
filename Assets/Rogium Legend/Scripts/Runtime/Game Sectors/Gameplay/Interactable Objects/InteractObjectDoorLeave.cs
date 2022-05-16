using System;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Rooms;
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
        public static event Action<RoomType, Vector2> OnTrigger; 

        private Vector2 direction;
        private RoomType nextRoomType;
        
        public void Construct(ObjectAsset data, ParameterInfo parameters)
        {
            direction = RedRatUtils.DirectionTypeToVector((DirectionType)parameters.intValue1);
            nextRoomType = (RoomType)parameters.intValue2;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController player))
            {
                if (player.ActionsLocked) return;
                OnTrigger?.Invoke(nextRoomType, direction);
            }
        }
    }
}