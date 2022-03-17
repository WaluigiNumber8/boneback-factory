using Rogium.Editors.Objects;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Entities.Player;
using UnityEngine;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// Allows the player to move on to the next room.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractObjectDoorLeave : MonoBehaviour, IInteractObject
    {
        public void Construct(ObjectAsset data)
        {
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController player))
            {
                if (player.ActionsLocked) return;
                GameplayOverseer.Instance.LoadNextNormalRoom();
                player.ForceMove(Vector2.down, 1, 0.2f);
            }
        }
    }
}