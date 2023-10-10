using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.FeelExtension
{
    /// <summary>
    /// Calls out different events for a <see cref="Selectable"/>.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class InteractableEventCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        public event Action OnClick;
        public event Action OnSelect;
        public event Action OnDeselect;
        public event Action OnClickDown;
        public event Action OnClickUp;

        public void OnPointerEnter(PointerEventData eventData) => WhenSelect();
        public void OnPointerExit(PointerEventData eventData) => WhenDeselect();
        public void OnPointerDown(PointerEventData eventData) => WhenClick();
        public void OnPointerUp(PointerEventData eventData) => OnClickUp?.Invoke();
        void ISelectHandler.OnSelect(BaseEventData eventData) => WhenSelect();
        void IDeselectHandler.OnDeselect(BaseEventData eventData) => WhenDeselect();
        public void OnSubmit(BaseEventData eventData) => WhenClick();

        /// <summary>
        /// Is called when the interactable is selected.
        /// </summary>
        protected virtual void WhenSelect() => OnSelect?.Invoke();
        /// <summary>
        /// Is called when the interactable is deselected.
        /// </summary>
        protected virtual void WhenDeselect() => OnDeselect?.Invoke();
        /// <summary>
        /// Is called when the interactable is clicked.
        /// </summary>
        protected virtual void WhenClick()
        {
            OnClick?.Invoke();
            OnClickDown?.Invoke();
        }

    }
}