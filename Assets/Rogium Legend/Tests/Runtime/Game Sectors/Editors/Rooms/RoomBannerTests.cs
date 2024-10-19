using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.GridSystem;
using Rogium.Tests.Core;
using UnityEngine;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Tests for the <see cref="RoomAsset"/>'s banner and icon separation.
    /// </summary>
    public class RoomBannerTests : MenuTestBase
    {
        private PackAsset pack;
        private RoomAsset room;
        private SpriteDrawer drawer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return AssetCreator.CreateAndAssignPack();
            drawer = new SpriteDrawer(EditorDefaults.Instance.RoomSize, new Vector2Int(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize), EditorDefaults.Instance.SpriteSize);
            pack = PackEditorOverseer.Instance.CurrentPack;
            room = new RoomAsset.Builder()
                .WithTileGrid(new ObjectGrid<AssetData>(EditorDefaults.Instance.RoomSize.x, EditorDefaults.Instance.RoomSize.y, () => new AssetData()))
                .Build();
            room.TileGrid.SetTo(0, 0, AssetDataBuilder.ForTile(pack.Tiles[0]));
        }
        
        [Test]
        public void Should_UpdateBanner()
        {
            Sprite bannerSprite = drawer.Build(room.TileGrid, pack.Tiles);
            room.UpdateBanner(bannerSprite);
            Assert.That(room.Banner, Is.EqualTo(bannerSprite));
        }
    }
}