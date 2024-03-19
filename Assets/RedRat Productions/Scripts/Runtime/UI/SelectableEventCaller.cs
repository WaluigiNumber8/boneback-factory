using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Calls out different events for a <see cref="Selectable"/>.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class SelectableEventCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        private const float BlockTime = 0.1f;
        
        public event Action OnClick;
        public event Action OnSelect;
        public event Action OnDeselect;
        public event Action OnClickDown;
        public event Action OnClickUp;

        private bool wasSelected;
        private bool cannotTriggerEvents;

        public void OnPointerEnter(PointerEventData eventData)
        {
            wasSelected = true;
            WhenSelect();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            wasSelected = false;
            WhenDeselect();
        }

        public void OnPointerDown(PointerEventData eventData) => WhenClick();
        public void OnPointerUp(PointerEventData eventData) => OnClickUp?.Invoke();
        void ISelectHandler.OnSelect(BaseEventData eventData) => WhenSelect();
        void IDeselectHandler.OnDeselect(BaseEventData eventData) => WhenDeselect();
        public void OnSubmit(BaseEventData eventData) => WhenClick();

        /// <summary>
        /// Is called when the interactable is selected.
        /// </summary>
        protected virtual void WhenSelect()
        {
            if (cannotTriggerEvents) return;
            OnSelect?.Invoke();
            StartCoroutine(BlockCoroutine(true));
        }

        /// <summary>
        /// Is called when the interactable is deselected.
        /// </summary>
        protected virtual void WhenDeselect()
        {
            if (cannotTriggerEvents) return;
            OnDeselect?.Invoke();
            StartCoroutine(BlockCoroutine(false));
        }

        /// <summary>
        /// Is called when the interactable is clicked.
        /// </summary>
        protected virtual void WhenClick()
        {
            OnClick?.Invoke();
            OnClickDown?.Invoke();
        }
        
        private IEnumerator BlockCoroutine(bool isSelected)
        {
            cannotTriggerEvents = true;
            yield return new WaitForSeconds(BlockTime);
            cannotTriggerEvents = false;
            if (isSelected && !wasSelected) OnDeselect?.Invoke();
            
        }
    }
}