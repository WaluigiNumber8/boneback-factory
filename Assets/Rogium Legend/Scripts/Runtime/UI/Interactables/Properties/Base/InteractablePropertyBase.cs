using UnityEngine;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all interactable properties.
    /// </summary>
    public abstract class InteractablePropertyBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected GameObject middleSpace;

        /// <summary>
        /// Changes, if the user can edit the property.
        /// </summary>
        /// <param name="isDisabled">When on, the property is disabled and cannot be edited.</param>
        public abstract void SetDisabled(bool isDisabled);
        
        protected void ConstructTitle(string titleText)
        {
            if (title == null) return;
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (middleSpace != null) middleSpace.SetActive((titleText != ""));
        }
        
        public string Title { get => title.text; }
    }
}