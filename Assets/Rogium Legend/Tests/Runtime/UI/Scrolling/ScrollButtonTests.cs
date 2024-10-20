using System.Collections;
using NUnit.Framework;
using RedRats.Core;
using RedRats.UI.Core.Scrolling;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Rogium.Tests.UI.Scrolling
{
    /// <summary>
    /// Tests for scrolling <see cref="ScrollRect"/>s with buttons.
    /// </summary>
    public class ScrollButtonTests : MenuTestBase
    {
        private ScrollRectButtonAdapter adapter;
        private ScrollRect scroll;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenuV2();
            adapter = SelectionMenuOverseerMono.GetInstance().GetComponentInChildren<ScrollRectButtonAdapter>();
            scroll = adapter.ScrollRect;
            scroll.horizontalNormalizedPosition = 0.5f;
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ScrollCategoryTabsRight_WhenScrollRightPressed()
        {
            float initialX = scroll.horizontalNormalizedPosition;
            adapter.ScrollRight();
            yield return new WaitForSeconds(0.1f);
            Assert.That(scroll.horizontalNormalizedPosition, Is.GreaterThan(initialX));
        }
        
        [UnityTest]
        public IEnumerator Should_StopScrollCategoryTabsRight_WhenScrollRightReleased()
        {
            adapter.ScrollRight();
            yield return new WaitForSeconds(0.1f);
            adapter.StopScrolling();
            float stoppedValue = scroll.horizontalNormalizedPosition;
            yield return new WaitForSeconds(0.1f);
            Assert.That(scroll.horizontalNormalizedPosition, Is.EqualTo(stoppedValue));
        }
        
        [UnityTest]
        public IEnumerator Should_ScrollCategoryTabsLeft_WhenScrollLeftPressed()
        {
            float initialX = scroll.horizontalNormalizedPosition;
            adapter.ScrollLeft();
            yield return new WaitForSeconds(0.1f);
            Assert.That(scroll.horizontalNormalizedPosition, Is.LessThan(initialX));
        }
        
        [UnityTest]
        public IEnumerator Should_StopScrollCategoryTabsLeft_WhenScrollLeftReleased()
        {
            adapter.ScrollLeft();
            yield return new WaitForSeconds(0.1f);
            adapter.StopScrolling();
            float stoppedValue = scroll.horizontalNormalizedPosition;
            yield return new WaitForSeconds(0.1f);
            Assert.That(scroll.horizontalNormalizedPosition, Is.EqualTo(stoppedValue));
        }

        [UnityTest]
        public IEnumerator Should_HideScrollRightButton_WhenReachedEndOfScroll()
        {
            adapter.ScrollRight();
            yield return new WaitForSeconds(2f);
            Assert.That(scroll.horizontalNormalizedPosition.IsSameAs(1f), Is.True);
            Assert.That(adapter.IsScrollRightButtonHidden, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_HideScrollLeftButton_WhenReachedStartOfScroll()
        {
            adapter.ScrollLeft();
            yield return new WaitForSeconds(2f);
            Assert.That(scroll.horizontalNormalizedPosition.IsSameAs(0f), Is.True);
            Assert.That(adapter.IsScrollLeftButtonHidden, Is.True);
        }
    }
}