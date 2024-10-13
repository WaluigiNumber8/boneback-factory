using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Refreshes the currently selected card if it's values were updated.
    /// </summary>
    public class SelectedCardRefresher : MonoBehaviour
    {
        private SelectionMenuOverseerMono selectionMenu;

        private void Awake() => selectionMenu = SelectionMenuOverseerMono.GetInstance();

        private void OnEnable()
        {
            PaletteEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardPalette;
            SpriteEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardSprite;
            WeaponEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardWeapon;
            ProjectileEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardProjectile;
            EnemyEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardEnemy;
            RoomEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardRoom;
            TileEditorOverseer.Instance.OnCompleteEditing += RefreshSelectedCardTile;
        }
        
        private void OnDisable()
        {
            PaletteEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardPalette;
            SpriteEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardSprite;
            WeaponEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardWeapon;
            ProjectileEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardProjectile;
            EnemyEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardEnemy;
            RoomEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardRoom;
            TileEditorOverseer.Instance.OnCompleteEditing -= RefreshSelectedCardTile;
        }
        
        private void RefreshSelectedCardPalette(IAsset _, int index)
        {
            if (selectionMenu.CurrentType != AssetType.Palette) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardSprite(IAsset _, int index, string __)
        {
            if (selectionMenu.CurrentType != AssetType.Sprite) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardWeapon(IAsset _, int index, string __)
        {
            if (selectionMenu.CurrentType != AssetType.Weapon) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardProjectile(IAsset _, int index, string __)
        {
            if (selectionMenu.CurrentType != AssetType.Projectile) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardEnemy(IAsset _, int index, string __)
        {
            if (selectionMenu.CurrentType != AssetType.Enemy) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardRoom(IAsset _, int index)
        {
            if (selectionMenu.CurrentType != AssetType.Room) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }
        
        private void RefreshSelectedCardTile(IAsset _, int index, string __)
        {
            if (selectionMenu.CurrentType != AssetType.Tile) return;
            selectionMenu.CurrentSelector.TryRefreshCard(index);
        }

    }
}