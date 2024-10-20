using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.UI.Core.Scrolling
{
    /// <summary>
    /// Allows a <see cref="ScrollRect"/> to be scrolled by buttons.
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectButtonAdapter : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.01f;
        [SerializeField] private HoldableButton scrollRightButton;
        
        private ScrollRect scrollRect;
        private Action scrollAction;
        
        private void Awake() => scrollRect = GetComponent<ScrollRect>();

        private void OnEnable()
        {
            scrollRightButton.OnHoldStart += ScrollRight;
            
            scrollRightButton.OnHoldEnd += StopScrolling;
        }

        private void OnDisable()
        {
            scrollRightButton.OnHoldStart -= ScrollRight;
            
            scrollRightButton.OnHoldEnd -= StopScrolling;
        }

        private void Update()
        {
            scrollAction?.Invoke();
        }

        public void ScrollRight() => scrollAction = () => scrollRect.horizontalNormalizedPosition += scrollSpeed;
        public void StopScrolling() => scrollAction = null;

        public ScrollRect ScrollRect { get => scrollRect; }
        
    }
}