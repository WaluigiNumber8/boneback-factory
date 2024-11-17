using System.Collections;
using NSubstitute;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
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
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Editors;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the UI scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class MenuTestWithInputBase : InputTestFixture
    {
        protected Keyboard keyboard;
        protected Mouse mouse;
        protected Gamepad gamepad;

        public override void Setup()
        {
            base.Setup();
            mouse = InputSystem.AddDevice<Mouse>();
            keyboard = InputSystem.AddDevice<Keyboard>();
            gamepad = InputSystem.AddDevice<Gamepad>();
            Press(mouse.leftButton);
            Release(mouse.leftButton);
            Press(keyboard.spaceKey);
            Release(keyboard.spaceKey);
            Press(gamepad.buttonSouth);
            Release(gamepad.buttonSouth);
        }

        public override void TearDown()
        {
            InputSystem.RemoveDevice(mouse);
            InputSystem.RemoveDevice(keyboard);
            InputSystem.RemoveDevice(gamepad);
            base.TearDown();
        }
        
        [UnitySetUp]
        public virtual IEnumerator SetUp()
        {
            SceneLoader.LoadMenuTestingScene();
            yield return null;
            PrepareExternalStorageSubstitute();
            yield return null;
            ExternalLibraryOverseer.Instance.ClearPacks();
            ExternalLibraryOverseer.Instance.ClearCampaigns();
            ActionHistorySystem.ClearHistory();
            yield return AssetCreator.CreateAndAssignPack();
        }
        
        private static void PrepareExternalStorageSubstitute()
        {
            IExternalStorageOverseer ex = Substitute.For<IExternalStorageOverseer>();
            ex.Packs.Returns(Substitute.For<ICRUDOperations<PackAsset, JSONPackAsset>>());
            ex.Packs.Load(Arg.Any<PackAsset>()).Returns(info => info.Arg<PackAsset>());
            ex.Palettes.Returns(Substitute.For<ICRUDOperations<PaletteAsset, JSONPaletteAsset>>());
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