using System;
using System.Collections;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Tiles;
using Rogium.Systems.ActionHistory;
using Rogium.UserInterface.Interactables.Properties;
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
        private RoomPropertyColumnBuilderObject builderObject;
        private RoomPropertyColumnBuilderEnemy builderEnemy;
        private RoomSettingsBuilder builderSettings;

        private AssetType currentType;
        private AssetData currentData;

        private void Awake()
        {
            builderTile = new RoomPropertyColumnBuilderTile(assetContent);
            builderObject = new RoomPropertyColumnBuilderObject(assetContent);
            builderEnemy = new RoomPropertyColumnBuilderEnemy(assetContent);
            builderSettings = new RoomSettingsBuilder();
        }

        private void OnEnable() => ActionHistorySystem.OnUpdateUndoHistory += RefreshProperties;
        private void OnDisable() => ActionHistorySystem.OnUpdateUndoHistory -= RefreshProperties;


        /// <summary>
        /// Load asset data into the column.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        /// <param name="type">The asset type.</param>
        public void ConstructAsset(IAsset asset, AssetType type)
        {
            ui.assetTitleText.text = asset.Title;
            ui.iconImage.color = EditorDefaults.Instance.DefaultColor;
            ui.iconImage.sprite = asset.Icon;

            string typeText = (type == AssetType.Tile) ? ((TileAsset) asset).Type.ToString() : type.ToString();
            ui.typeText.text = typeText;
            currentType = type;
        }

        /// <summary>
        /// Constructs the Asset Properties Column from tiles.
        /// </summary>
        /// <param name="data">The asset data to read from.</param>
        public void ConstructAssetPropertiesTile(AssetData data)
        {
            builderTile.Build(data);
            currentData = data;
        }

        /// <summary>
        /// Constructs the Asset Properties Column from interactable objects.
        /// </summary>
        /// <param name="data">The asset data to read from.</param>
        public void ConstructAssetPropertiesObject(AssetData data)
        {
            builderObject.Build(data);
            currentData = data;
        }

        /// <summary>
        /// Constructs the Asset Properties Column from enemies.
        /// </summary>
        /// <param name="data">The asset data to read from.</param>
        public void ConstructAssetPropertiesEnemies(AssetData data)
        {
            builderEnemy.Build(data);
            currentData = data;
        }

        /// <summary>
        /// Constructs the Room Settings Column.
        /// </summary>
        public void ConstructSettings(RoomAsset data)
        {
            ui.roomTitleText.text = data.Title;
            builderSettings.Build(settingsContent, data, true);
        }

        /// <summary>
        /// Clears all set data.
        /// </summary>
        public void ConstructEmpty()
        {
            assetContent.ReleaseAllProperties();
            ui.assetTitleText.text = defaultText;
            ui.typeText.text = "";
            ui.iconImage.color = EditorDefaults.Instance.NoColor;
            ui.iconImage.sprite = null;
            
            currentType = AssetType.None;
            currentData = new AssetData();
        }

        /// <summary>
        /// Prepares the Properties Column for Asset Properties visually.
        /// </summary>
        public void PreparePropertiesColumn()
        {
            ui.assetTitleText.gameObject.SetActive(true);
            ui.roomTitleText.gameObject.SetActive(false);
            ui.typeText.gameObject.SetActive(true);
            ui.icon.gameObject.SetActive(true);
        }

        /// <summary>
        /// Prepares the column for room settings visually.
        /// </summary>
        public void PrepareSettingsColumn()
        {
            ui.assetTitleText.gameObject.SetActive(false);
            ui.roomTitleText.gameObject.SetActive(true);
            ui.typeText.gameObject.SetActive(false);
            ui.icon.SetActive(false);
        }

        private void RefreshProperties()
        {
            if (currentData.IsEmpty()) return;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                switch (currentType)
                {
                    case AssetType.Enemy:
                        ConstructAssetPropertiesEnemies(currentData);
                        break;
                    case AssetType.Tile:
                        ConstructAssetPropertiesTile(currentData);
                        break;
                    case AssetType.Object:
                        ConstructAssetPropertiesObject(currentData);
                        break;
                    default: throw new NotSupportedException($"Asset type '{currentType}' is not supported.");
                }
            }
        }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI assetTitleText;
            public TextMeshProUGUI roomTitleText;
            public TextMeshProUGUI typeText;
            public GameObject icon;
            public Image iconImage;
        }
    }
}