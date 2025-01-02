using UnityEngine;

namespace RedRats.Core.Helpers
{
    /// <summary>
    /// Allows for toggling the active status of a game object.
    /// </summary>
    public class ActiveStatusToggler : MonoBehaviour
    {
        public void ToggleActiveStatus() => gameObject.SetActive(!gameObject.activeSelf);
    }
}