using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Calls out different events for a <see cref="Selectable"/>.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class InteractableEventCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        public event Action OnClick;
        public event Action OnSelect;
        public event Action OnDeselect;

        public void OnPointerEnter(PointerEventData eventData) => OnSelect?.Invoke();
        public void OnPointerExit(PointerEventData eventData) => OnDeselect?.Invoke();
        public void OnPointerDown(PointerEventData eventData) => OnClick?.Invoke();
        void ISelectHandler.OnSelect(BaseEventData eventData) => OnSelect?.Invoke();
        void IDeselectHandler.OnDeselect(BaseEventData eventData) => OnDeselect?.Invoke();
        public void OnSubmit(BaseEventData eventData) => OnClick?.Invoke();
    }
}