using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Calls out different events for a Selectable object.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class InteractableEventCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        private const float BlockTime = 0.1f;

        public event Action OnSelect, OnDeselect;
        public event Action OnClickLeft, OnClickLeftDown, OnClickLeftUp;
        public event Action OnClickRight, OnClickRightDown, OnClickRightUp;
        public event Action OnClickDisabled;

        private Selectable selectable;
        
        private bool wasSelected;
        private bool cannotTriggerEvents;

        private void Awake() => selectable = GetComponent<Selectable>();

        private void OnDisable()
        {
            cannotTriggerEvents = false;
            wasSelected = false;
        }

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

        public void OnPointerClick(PointerEventData eventData) => WhenClick(eventData.button);
        public void OnPointerDown(PointerEventData eventData) => WhenClickDown(eventData.button);
        public void OnPointerUp(PointerEventData eventData) => WhenClickUp(eventData.button);
        void ISelectHandler.OnSelect(BaseEventData eventData) => WhenSelect();
        void IDeselectHandler.OnDeselect(BaseEventData eventData) => WhenDeselect();
        public void OnSubmit(BaseEventData eventData) => WhenClick(PointerEventData.InputButton.Left);

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
        protected virtual void WhenClick(PointerEventData.InputButton button)
        {
            if (!selectable.interactable)
            {
                OnClickDisabled?.Invoke();
                return;
            }
            
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    OnClickLeft?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickRight?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                default: throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
        
        /// <summary>
        /// Is called when the interactable is clicked down anywhere on screen.
        /// </summary>
        protected virtual void WhenClickDown(PointerEventData.InputButton button)
        {
            if (!selectable.interactable) return;
            
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    OnClickLeftDown?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickRightDown?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                default: throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
        
        /// <summary>
        /// Is called when the interactable is clicked up anywhere on screen.
        /// </summary>
        protected virtual void WhenClickUp(PointerEventData.InputButton button)
        {
            if (!selectable.interactable) return;
            
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    OnClickLeftUp?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnClickRightUp?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                default: throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }

        private IEnumerator BlockCoroutine(bool isSelected)
        {
            cannotTriggerEvents = true;
            yield return new WaitForSecondsRealtime(BlockTime);
            cannotTriggerEvents = false;
            if (isSelected && !wasSelected) OnDeselect?.Invoke();
        }
    }
}