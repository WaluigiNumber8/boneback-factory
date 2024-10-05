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

        [UnityTest]
        public IEnumerator Should_ContainPack0Reference_WhenMultipleSelectedAndConfirmSelection()
        {
            editorMono.SelectionPicker.Select(0);
            editorMono.SelectionPicker.Select(1);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[0].ID), Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_ContainPack1Reference_WhenMultipleSelectedAndConfirmSelection()
        {
            editorMono.SelectionPicker.Select(0);
            editorMono.SelectionPicker.Select(1);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[1].ID), Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_ContainPack0Reference_WhenOneWasToggledOffAndConfirmSelection()
        {
            editorMono.SelectionPicker.Select(0);
            editorMono.SelectionPicker.Select(1);
            yield return null;
            editorMono.SelectionPicker.Select(1, false);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[0].ID), Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_NotContainPack1Reference_WhenItWasToggledOffAndConfirmSelection()
        {
            editorMono.SelectionPicker.Select(0);
            editorMono.SelectionPicker.Select(1);
            yield return null;
            editorMono.SelectionPicker.Select(1, false);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[1].ID), Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_NotContainAnything_WhenNothingIsSelectedAndConfirmSelection()
        {
            editorMono.SelectionPicker.Select(0, false);
            editorMono.SelectionPicker.Select(1, false);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[0].ID), Is.False);
            Assert.That(editor.CurrentAsset.PackReferences.Contains(ExternalLibraryOverseer.Instance.Packs[1].ID), Is.False);
        }

        [UnityTest]
        public IEnumerator Should_GetAllAssets_WhenSelectAll()
        {
            editorMono.SelectionPicker.SelectAll(true);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Count, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs.Count));
        }

        [UnityTest]
        public IEnumerator Should_NotGetAnyAssets_WhenSelectAllAndDeselectAll()
        {
            editorMono.SelectionPicker.SelectAll(true);
            yield return null;
            editorMono.SelectionPicker.SelectAll(false);
            yield return null;
            editorMono.SelectionPicker.ConfirmSelection();
            yield return null;
            Assert.That(editor.CurrentAsset.PackReferences.Count, Is.EqualTo(0));
        }
    }
}