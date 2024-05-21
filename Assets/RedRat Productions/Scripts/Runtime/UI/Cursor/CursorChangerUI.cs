using UnityEngine;
using UnityEngine.EventSystems;

namespace RedRats.UI.Core.Cursors
{
    /// <summary>
    /// Changes the cursor to a type when hovering over a UI element.
    /// </summary>
    public class CursorChangerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CursorType type = CursorType.Interact;
        private CursorOverseerMono overseer;

        private void Awake() => overseer = CursorOverseerMono.GetInstance();

        public void OnPointerEnter(PointerEventData eventData) => overseer.Set(type);

        public void OnPointerExit(PointerEventData eventData) => overseer.Set(CursorType.Default);
    }
}