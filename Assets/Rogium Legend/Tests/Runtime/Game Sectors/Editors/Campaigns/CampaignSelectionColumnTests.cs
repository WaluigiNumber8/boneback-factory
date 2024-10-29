using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.Campaigns
{
    /// <summary>
    /// Tests for the <see cref="SelectionInfoColumn"/> in the Campaign Editor.
    /// </summary>
    public class CampaignSelectionColumnTests : MenuTestBase
    {
        private CampaignEditorOverseerMono campaignEditor;
        private SelectionInfoColumn infoColumn;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return null;
            yield return MenuLoader.PrepareCampaignEditor();
            campaignEditor = CampaignEditorOverseerMono.GetInstance();
            infoColumn = campaignEditor.GetComponentInChildren<SelectionInfoColumn>();
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ShowEmptyColumn_When_OpenEditorWithEmptyCampaign()
        {
            yield return null;
            Assert.That(infoColumn.Title, Is.EqualTo("Select a pack"));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackTitle_WhenPackCardClicked()
        {
            yield return CampaignSelectionColumnTestsU.SelectCard();
            Assert.That(infoColumn.Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Title));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowPackTitle_WhenPackCardDeselected()
        {
            yield return CampaignSelectionColumnTestsU.SelectCard();
            yield return CampaignSelectionColumnTestsU.SelectCard(1);
            yield return CampaignSelectionColumnTestsU.DeselectCard();
            Assert.That(infoColumn.Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackIcon_WhenPackCardClicked()
        {
            yield return CampaignSelectionColumnTestsU.SelectCard();
            Assert.That(infoColumn.Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackDescription_WhenPackCardClicked()
        {
            yield return CampaignSelectionColumnTestsU.SelectCard();
            Assert.That(infoColumn.Description, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Description));
        }

        [UnityTest]
        public IEnumerator Should_ShowOnly3Properties_WhenPackCardClicked()
        {
            yield return CampaignSelectionColumnTestsU.SelectCard();
            Assert.That(infoColumn.PropertiesCount, Is.EqualTo(3));
        }
    }
}