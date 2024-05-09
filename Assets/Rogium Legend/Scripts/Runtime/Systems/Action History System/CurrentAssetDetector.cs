using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Options.Core;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Detects the currently edited asset.
    /// </summary>
    public class CurrentAssetDetector : Singleton<CurrentAssetDetector>
    {
        private string currentAssetID;
        private string lastAssetID;

        private CurrentAssetDetector()
        {
            PaletteEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            SpriteEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            WeaponEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            ProjectileEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            EnemyEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            RoomEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            TileEditorOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
            OptionsMenuOverseer.Instance.OnAssignAsset += SetCurrentAssetID;
        }

        private void SetCurrentAssetID(IIDHolder assetID)
        {
            lastAssetID = currentAssetID;
            currentAssetID = assetID.ID;
        }

        /// <summary>
        /// Returns TRUE if a different asset is being edited.
        /// </summary>
        public bool WasAssetChanged
        {
            get
            {
                bool wasChanged = lastAssetID != currentAssetID;
                if (wasChanged) lastAssetID = currentAssetID;
                return wasChanged;
            }
        }

        public string CurrentAssetID { get => currentAssetID; }
    }
}