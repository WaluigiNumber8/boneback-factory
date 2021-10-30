using UnityEngine;
using UnityEngine.EventSystems;

namespace BoubakProductions.UI.Core
{
    /// <summary>
    /// Fixes Unity's weird behaviour when moving the pointer away from the UI Element
    /// </summary>
    public class UIElementSelectionFixer : MonoBehaviour, IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}