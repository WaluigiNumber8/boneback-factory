using System.Collections;
using NSubstitute;
using Rogium.Editors.Campaign;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.ExternalStorage;
using Rogium.ExternalStorage.Serialization;
using Rogium.Options.Core;
using Rogium.Tests.Editors;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the Menu test scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class MenuTestBase
    {
        [UnitySetUp]
        public virtual IEnumerator Setup()
        {
            SceneLoader.LoadMenuTestingScene();
            yield return null;
            PrepareExternalStorageSubstitute();
            yield return null;
            yield return AssetCreator.CreateAndAssignPack();
        }

        private void PrepareExternalStorageSubstitute()
        {
            IExternalStorageOverseer ex = Substitute.For<IExternalStorageOverseer>();
            ex.LoadPack(Arg.Any<PackAsset>()).Returns(info => info.Arg<PackAsset>());
            ex.Palettes.Returns(Substitute.For<ICRUDOperations<PaletteAsset, JSONPaletteAsset>>());
            ex.Sprites.Returns(Substitute.For<ICRUDOperations<SpriteAsset, JSONSpriteAsset>>());
            ex.Enemies.Returns(Substitute.For<ICRUDOperations<EnemyAsset, JSONEnemyAsset>>());
            ex.Projectiles.Returns(Substitute.For<ICRUDOperations<ProjectileAsset, JSONProjectileAsset>>());
            ex.Weapons.Returns(Substitute.For<ICRUDOperations<WeaponAsset, JSONWeaponAsset>>());
            ex.Rooms.Returns(Substitute.For<ICRUDOperations<RoomAsset, JSONRoomAsset>>());
            ex.Tiles.Returns(Substitute.For<ICRUDOperations<TileAsset, JSONTileAsset>>());
            ex.Campaigns.Returns(Substitute.For<ICRUDOperations<CampaignAsset, JSONCampaignAsset>>());
            ex.Preferences.Returns(Substitute.For<ICRUDOperations<GameDataAsset, JSONGameDataAsset>>());
            ExternalCommunicator.Instance.OverrideStorageOverseer(ex);
        }
    }
}