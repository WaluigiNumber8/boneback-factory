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
        public void Clear() => transform.ReleaseAllProperties();

        public override void ReleaseToPool()
        {
            transform.ReleaseAllProperties();
            base.ReleaseToPool();
        }

        public Transform GetTransform => transform;
    }
}