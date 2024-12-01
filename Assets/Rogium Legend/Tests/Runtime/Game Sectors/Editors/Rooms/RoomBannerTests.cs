using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.AssetSelection;
using Rogium.Editors.AssetSelection.Campaigns;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.GASExtension;
using Rogium.Systems.GridSystem;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.Rooms.RoomBannerTestsU;
using SelectionInfoColumn = Rogium.Editors.AssetSelection.SelectionInfoColumn;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Tests for the <see cref="RoomAsset"/>'s banner and icon separation.
    /// </summary>
    public class RoomBannerTests : MenuTestBase
    {
        private PackEditorOverseer packEditor;
        private SpriteDrawer drawer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return AssetCreator.CreateAndAssignPack();
            yield return MenuLoader.PrepareRoomEditor();
            drawer = new SpriteDrawer(EditorDefaults.Instance.RoomSize, new Vector2Int(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize), EditorDefaults.Instance.SpriteSize);
            packEditor = PackEditorOverseer.Instance;
        }
        
        [Test]
        public void Should_UpdateBanner()
        {
            RoomAsset room = packEditor.CurrentPack.Rooms[0];
            Sprite bannerSprite = drawer.Draw(room.TileGrid, packEditor.CurrentPack.Tiles);
            room.UpdateBanner(bannerSprite);
            Assert.That(room.Banner, Is.EqualTo(bannerSprite));
        }

        [UnityTest]
        public IEnumerator Should_UpdateBanner_WhenCompleteEditing()
        {
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(0, 0));
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(1, 0));
            RoomEditorOverseer.Instance.CompleteEditing();
            yield return null;
            Assert.That(packEditor.CurrentPack.Rooms[0].Banner, Is.Not.Null);
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateBannerWithRoomLayoutSprite_WhenCompleteEditing()
        {
            yield return SelectRoomAndUpdateTileGridThenSave();
            RoomAsset room = packEditor.CurrentPack.Rooms[0];
            Assert.That(room.Banner.texture.GetPixel(0, 0), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Tiles[0].Icon.texture.GetPixel(0, 0)));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBannerInSelectionMenuInsteadOfIcon()
        {
            yield return SelectRoomAndUpdateTileGridThenSave();
            Assert.That(SelectionMenuOverseerMono.GetInstance().GetComponentInChildren<SelectionInfoColumn>().BannerIcon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Banner));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBannerInCampaignSelectionInsteadOfIcon()
        {
            AssetCreator.AddNewCampaignToLibrary();
            ExternalLibraryOverseer.Instance.Packs[0].Rooms[0].UpdateType(RoomType.Entrance);
            yield return null;
            OverseerLoader.LoadModalWindowBuilder();
            yield return MenuLoader.PrepareCampaignSelection();
            yield return MenuLoader.PrepareCampaignEditor(false);
            GASButtonActions.OpenSelectionCampaign();
            GASButtonActions.OpenEditorCampaign(0);
            yield return null;
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.Select(0);
            GASButtonActions.SaveChangesCampaign();
            yield return null;
            Object.FindFirstObjectByType<ModalWindow>().OnAccept();
            yield return null;
            Assert.That(CampaignAssetSelectionOverseerMono.GetInstance().Wallpaper.BannerIcon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Banner));
        }

        [UnityTest]
        public IEnumerator Should_GenerateRoomIconWithSameDimensionsAsRoomSize_WhenCompleteEditing()
        {
            yield return SelectRoomAndUpdateTileGridThenSave();
            RoomAsset room = packEditor.CurrentPack.Rooms[0];
            Assert.That(room.Icon.texture.width, Is.EqualTo(EditorDefaults.Instance.RoomSize.x));
            Assert.That(room.Icon.texture.height, Is.EqualTo(EditorDefaults.Instance.RoomSize.y));
        }
        
        [UnityTest]
        public IEnumerator Should_GenerateRoomIconThatRepresentsTiles_WhenCompleteEditing()
        {
            yield return SelectRoomAndUpdateTileGridThenSave();
            RoomAsset room = packEditor.CurrentPack.Rooms[0];
            Assert.That(room.Icon.texture.GetPixel(0, 0), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Tiles.FindValueFirst(room.TileGrid.GetAt(0, 0)).Icon.texture.GetPixel(8, 8)));
        }
    }
}