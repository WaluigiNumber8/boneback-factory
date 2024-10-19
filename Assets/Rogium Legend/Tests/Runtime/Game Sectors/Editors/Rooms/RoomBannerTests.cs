using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.GridSystem;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Tests for the <see cref="RoomAsset"/>'s banner and icon separation.
    /// </summary>
    public class RoomBannerTests : MenuTestBase
    {
        private PackAsset pack;
        private SpriteDrawer drawer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return AssetCreator.CreateAndAssignPack();
            yield return MenuLoader.PrepareRoomEditor();
            drawer = new SpriteDrawer(EditorDefaults.Instance.RoomSize, new Vector2Int(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize), EditorDefaults.Instance.SpriteSize);
            pack = PackEditorOverseer.Instance.CurrentPack;
        }
        
        [Test]
        public void Should_UpdateBanner()
        {
            RoomAsset room = pack.Rooms[0];
            Sprite bannerSprite = drawer.Build(room.TileGrid, pack.Tiles);
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
            Assert.That(pack.Rooms[0].Banner, Is.Not.Null);
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateBannerWithRoomLayoutSprite_WhenCompleteEditing()
        {
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(0, 0));
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(1, 0));
            RoomEditorOverseer.Instance.CompleteEditing();
            yield return null;
            RoomAsset room = pack.Rooms[0];
            Assert.That(room.Banner.texture.GetPixel(0, 0), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Tiles[0].Icon.texture.GetPixel(0, 0)));
        }
    }
}