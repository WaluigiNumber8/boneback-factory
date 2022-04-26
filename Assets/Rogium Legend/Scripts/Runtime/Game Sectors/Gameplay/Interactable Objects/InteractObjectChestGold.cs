using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Gameplay.Entities.Player;
using UnityEngine;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// Upon the player's touch, throws out a random weapon.
    /// </summary>
    public class InteractObjectChestGold : MonoBehaviour, IInteractObject
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimatorPropertyInfo animatorProperties;
        
        private bool isOpen;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isOpen) return;
            if (!other.TryGetComponent(out PlayerController player)) return;
            Pick();
        }

        public void Construct(ObjectAsset data, ParameterInfo parameters)
        {
            
        }

        /// <summary>
        /// Picks the next item to be thrown out of the chest.
        /// </summary>
        private void Pick()
        {
            animator.SetTrigger(animatorProperties.open);
            isOpen = true;
        }

        [System.Serializable]
        public struct AnimatorPropertyInfo
        {
            public string open;
            public string close;
        }
    }
}