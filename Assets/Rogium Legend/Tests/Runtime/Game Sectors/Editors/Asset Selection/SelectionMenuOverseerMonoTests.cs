using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for working with the Asset Selection Menu.
    /// </summary>
    public class SelectionMenuOverseerMonoTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
        }

        [Test]
        public void Should_FillWithPackAssetCards_WhenOpenForPacks()
        {
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(3));
        }
    }
}