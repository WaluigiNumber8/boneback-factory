using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Tests.Core;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Campaigns
{
    /// <summary>
    /// Tests for selecting packs in the campaign editor.
    /// </summary>
    public class CampaignsSelectionTests : MenuTestBase
    {
        private CampaignEditorOverseer editor;
        private CampaignEditorOverseerMono editorMono;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            AssetCreator.AddNewPackToLibrary();
            AssetCreator.AddNewPackToLibrary();
            yield return MenuLoader.PrepareCampaignEditor();
            editor = CampaignEditorOverseer.Instance;
            editorMono = CampaignEditorOverseerMono.GetInstance();
        }

        [UnityTest]
        public IEnumerator Should_LoadAllPacksIntoContent_WhenEditorOpened()
        {
            yield return null;
            Assert.That(editorMono.SelectionPicker.SelectorContent.childCount, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs.Count));
        }
    }
}