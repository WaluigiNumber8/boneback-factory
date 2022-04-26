using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyContentBlock : InteractablePropertyBase
    {
        [SerializeField] private new Transform transform;
        
        public override void SetDisabled(bool isDisabled) => transform.gameObject.SetActive(!isDisabled);

        /// <summary>
        /// Clears the content block's content.
        /// </summary>
        public void Clear() => transform.KillChildren();

        public Transform GetTransform => transform;
    }
}