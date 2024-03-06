using RedRats.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyContentBlock : InteractablePropertyBase<bool>
    {
        [SerializeField] private new Transform transform;
        
        public override void SetDisabled(bool isDisabled) => transform.gameObject.SetActive(!isDisabled);

        /// <summary>
        /// Clears the content block's content.
        /// </summary>
        public void Clear() => transform.KillChildren();

        public Transform GetTransform => transform;
        
        public override bool PropertyValue { get => false; }
    }
}