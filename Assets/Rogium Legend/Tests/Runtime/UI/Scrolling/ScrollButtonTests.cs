using System.Collections;
using NUnit.Framework;
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
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenuV2();
        }

        [UnityTest]
        public IEnumerator Should_ScrollCategoryTabsRight_WhenScrollRightPressed()
        {
            ScrollRectButtonAdapter adapter = SelectionMenuOverseerMono.GetInstance().GetComponentInChildren<ScrollRectButtonAdapter>();
            ScrollRect scroll = adapter.ScrollRect;
            float initialX = scroll.horizontalNormalizedPosition;
            adapter.ScrollRight();
            yield return new WaitForSeconds(0.1f);
            Assert.That(scroll.horizontalNormalizedPosition, Is.GreaterThan(initialX));
        }
    }
}