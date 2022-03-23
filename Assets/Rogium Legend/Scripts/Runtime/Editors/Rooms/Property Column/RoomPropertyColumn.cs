using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Editors.Rooms.PropertyColumn
{
    /// <summary>
    /// Controls the room property column.
    /// </summary>
    public class RoomPropertyColumn : MonoBehaviour
    {
        [SerializeField] private string defaultText;
        [SerializeField] private Transform assetContent;
        [SerializeField] private Transform settingsContent;
        [SerializeField] private UIInfo ui;

        private RoomPropertyColumnBuilderTile builderTile;
        private RoomPropertyColumnBuilderEnemy builderEnemy;
        private RoomSettingsBuilder builderSettings;

        private AssetType lastType = AssetType.Tile;
        private string currentRoomTitle;
        private string lastAssetTitle;
        
        private void Awake()
        {
            builderTile = new RoomPropertyColumnBuilderTile(assetContent);
            builderEnemy = new RoomPropertyColumnBuilderEnemy(assetContent);
            builderSettings = new RoomSettingsBuilder();
            
            ui.typeText.text = lastType.ToString();
        }

        /// <summary>
        /// Load asset data into the column.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        /// <param name="type">The asset type.</param>
        public void ConstructAsset(IAsset asset, AssetType type)
        {
            ui.titleText.text = asset.Title;
            ui.iconImage.color = EditorDefaults.DefaultColor;
            ui.iconImage.sprite = asset.Icon;

            lastAssetTitle = asset.Title;
            
            if (type == lastType && ui.typeText.text != "") return;
            ui.typeText.text = type.ToString();
            lastType = type;
        }

        /// <summary>
        /// Constructs the Asset Properties Column from tiles.
        /// </summary>
        /// <param name="data">The asset data to read from.</param>
        public void ConstructAssetPropertiesTile(AssetData data)
        {
            builderTile.Build(data);
        }
        
        /// <summary>
        /// Constructs the Asset Properties Column from enemies.
        /// </summary>
        /// <param name="data">The asset data to read from.</param>
        public void ConstructAssetPropertiesEnemies(AssetData data)
        {
            builderEnemy.Build(data);
        }
        
        /// <summary>
        /// Constructs the Room Settings Column.
        /// </summary>
        public void ConstructSettings(RoomAsset data)
        {
            builderSettings.Build(settingsContent, data, true);
            currentRoomTitle = data.Title;
        }
        
        /// <summary>
        /// Clears all set data.
        /// </summary>
        public void ConstructEmpty()
        {
            assetContent.gameObject.KillChildren();
            ui.titleText.text = defaultText;
            ui.typeText.text = "";
            ui.iconImage.color = EditorDefaults.NoColor;
            ui.iconImage.sprite = null;
        }

        /// <summary>
        /// Prepares the Properties Column for Asset Properties visually.
        /// </summary>
        public void PreparePropertiesColumn()
        {
            ui.titleText.text = lastAssetTitle;
            ui.typeText.gameObject.SetActive(true);
            ui.icon.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Prepares the column for room settings visually.
        /// </summary>
        public void PrepareSettingsColumn()
        {
            ui.titleText.text = currentRoomTitle;
            ui.typeText.gameObject.SetActive(false);
            ui.icon.SetActive(false);
        }
        
        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI titleText;
            public TextMeshProUGUI typeText;
            public GameObject icon;
            public Image iconImage;
        }
    }
}