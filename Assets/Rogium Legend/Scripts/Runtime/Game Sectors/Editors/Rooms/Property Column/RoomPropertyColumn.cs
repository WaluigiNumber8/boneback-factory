using System;
using System.Collections;
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
            builderSettings = new RoomSettingsBuilder(settingsContent);
        }

        private void OnEnable() => ActionHistorySystem.OnUpdateUndoHistory += Reconstruct;
        private void OnDisable() => ActionHistorySystem.OnUpdateUndoHistory -= Reconstruct;

        private void Reconstruct() => Construct(currentData);

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
        /// Constructs the Room Settings Column.
        /// </summary>
        public void ConstructSettings(RoomAsset data)
        {
            ui.roomTitleText.text = data.Title;
            builderSettings.Build(data);
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

        public void Dispose()
        {
            assetContent.ReleaseAllProperties();
            settingsContent.ReleaseAllProperties();
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

        public void Construct(AssetData data)
        {
            if (data.IsEmpty()) return;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                currentData = data;
                
                yield return new WaitForEndOfFrame();
                switch (currentType)
                {
                    case AssetType.Enemy:
                        builderEnemy.Build(currentData);
                        break;
                    case AssetType.Tile:
                        builderTile.Build(currentData);
                        break;
                    case AssetType.Object:
                        builderObject.Build(currentData);
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