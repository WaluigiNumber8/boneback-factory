using System;
using System.Collections.Generic;
using RedRats.Core;
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
        [SerializeField, Range(0f, 0.5f)] private float edgeTolerance = 0.05f;
        [SerializeField] private HoldableButton scrollRightButton;
        [SerializeField] private HoldableButton scrollLeftButton;
        
        private ScrollRect scrollRect;
        private IDictionary<DirectionType, ScrollInfo> scrollData;
        private ScrollInfo? currentScroll;
        
        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            scrollData = new Dictionary<DirectionType, ScrollInfo>
            {
                {DirectionType.Right, new ScrollInfo(scrollRightButton, () => scrollRect.horizontalNormalizedPosition += scrollSpeed * Time.deltaTime, () => scrollRect.normalizedPosition.x >= 1 - edgeTolerance)},
                {DirectionType.Left, new ScrollInfo(scrollLeftButton, () => scrollRect.horizontalNormalizedPosition -= scrollSpeed * Time.deltaTime, () => scrollRect.normalizedPosition.x <= 0 + edgeTolerance)}
            };
        }

        private void OnEnable()
        {
            scrollRightButton.OnHoldStart += ScrollRight;
            scrollRightButton.OnHoldEnd += StopScrolling;
            scrollLeftButton.OnHoldStart += ScrollLeft;
            scrollLeftButton.OnHoldEnd += StopScrolling;
        }

        private void OnDisable()
        {
            scrollRightButton.OnHoldStart -= ScrollRight;
            scrollRightButton.OnHoldEnd -= StopScrolling;
            scrollLeftButton.OnHoldStart -= ScrollLeft;
            scrollLeftButton.OnHoldEnd -= StopScrolling;
        }

        private void Update()
        {
            if (Time.frameCount % 3 != 0) return;
            RecalculateButtonStatus();
            if (currentScroll == null) return;
            currentScroll?.scrollAction();
            
            if (currentScroll?.canButtonHide() == false) return;
            StopScrolling();
        }

        public void ScrollRight() => currentScroll = scrollData[DirectionType.Right];

        public void ScrollLeft() => currentScroll = scrollData[DirectionType.Left];

        public void StopScrolling() => currentScroll = null;

        public bool IsScrollRightButtonHidden() => scrollData[DirectionType.Right].IsButtonHidden();
        public bool IsScrollLeftButtonHidden() => scrollData[DirectionType.Left].IsButtonHidden();
        
        private void RecalculateButtonStatus()
        {
            foreach (ScrollInfo data in scrollData.Values) data.ProcessButtonStatus();
        }
        
        public ScrollRect ScrollRect { get => scrollRect; }

        private readonly struct ScrollInfo
        {
            public readonly Action scrollAction;
            public readonly Func<bool> canButtonHide;
            public readonly HoldableButton button;
            
            public ScrollInfo(HoldableButton button, Action scrollAction, Func<bool> canButtonHide)
            {
                this.button = button;
                this.scrollAction = scrollAction;
                this.canButtonHide = canButtonHide;
            }
            
            public void ProcessButtonStatus() => button.gameObject.SetActive(!canButtonHide());
            
            public bool IsButtonHidden() => !button.gameObject.activeSelf;
        }
    }
}