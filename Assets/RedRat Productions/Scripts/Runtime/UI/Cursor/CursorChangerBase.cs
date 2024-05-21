using RedRats.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RedRats.UI.Core.Cursors
{
    /// <summary>
    /// A base for all cursor changers.
    /// </summary>
    public abstract class CursorChangerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform rectTransform;
        private CursorOverseerMono overseer;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            overseer = CursorOverseerMono.GetInstance();
        }

        protected virtual void OnEnable()
        {
            //Return if the mouse is not over this specific ui object.
            if (!RedRatUtils.IsPointerOverTransform(rectTransform)) return;
            overseer.Set(CursorToSet);
        }

        protected virtual void OnDisable() => overseer.Set(CursorType.Default);
        
        public void OnPointerEnter(PointerEventData eventData) => overseer.Set(CursorToSet);
        public void OnPointerExit(PointerEventData eventData) => overseer.Set(CursorType.Default);

        protected abstract CursorType CursorToSet { get; }

    }
}