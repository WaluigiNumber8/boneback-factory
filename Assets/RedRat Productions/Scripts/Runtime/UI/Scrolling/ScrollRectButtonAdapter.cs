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
                {DirectionType.Right, new ScrollInfo(scrollRightButton, () => scrollRect.horizontalNormalizedPosition += scrollSpeed, () => scrollRect.normalizedPosition.x >= 1f)},
                {DirectionType.Left, new ScrollInfo(scrollLeftButton, () => scrollRect.horizontalNormalizedPosition -= scrollSpeed, () => scrollRect.normalizedPosition.x <= 0f)}
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
            if (currentScroll == null) return;
            foreach (ScrollInfo data in scrollData.Values) data.TryShowButton();
            currentScroll?.scrollAction();
            
            if (currentScroll?.canButtonHide() == false) return;
            currentScroll?.HideButton();
            StopScrolling();
        }

        public void ScrollRight()
        {
            currentScroll = scrollData[DirectionType.Right];
            currentScroll?.TryShowButton();
        }
        
        public void ScrollLeft()
        {
            currentScroll = scrollData[DirectionType.Left];
            currentScroll?.TryShowButton();
        }

        public void StopScrolling() => currentScroll = null;

        public bool IsScrollRightButtonHidden() => !scrollRightButton.gameObject.activeSelf;
        public bool IsScrollLeftButtonHidden() => !scrollLeftButton.gameObject.activeSelf;
        public ScrollRect ScrollRect { get => scrollRect; }

        private readonly struct ScrollInfo
        {
            public readonly Action scrollAction;
            public readonly Func<bool> canButtonHide;
            private readonly HoldableButton button;
            
            public ScrollInfo(HoldableButton button, Action scrollAction, Func<bool> canButtonHide)
            {
                this.button = button;
                this.scrollAction = scrollAction;
                this.canButtonHide = canButtonHide;
            }
            
            public void TryShowButton()
            {
                button.gameObject.SetActive(true);
            }

            public void HideButton() => button.gameObject.SetActive(false);
        }
    }
}