using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core.Scrolling
{
    /// <summary>
    /// A button that can be held down to perform an action.
    /// </summary>
    public class HoldableButton : Button
    {
        public event Action OnHoldStart, OnHoldEnd;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnHoldStart?.Invoke();
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnHoldEnd?.Invoke();
        }
    }
}