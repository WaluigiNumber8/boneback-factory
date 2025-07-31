using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
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
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Controls the content of the Selection Menu Info Column.
    /// </summary>
    public class SelectionInfoColumn : MonoBehaviour
    {
        [SerializeField, Tooltip("Build only essential properties")] private bool essentialsOnly;
        [SerializeField] private UIInfo ui;

        private IDictionary<Type, Action<IAsset>> builders;
        private SelectionInfoColumnPropertyBuilderPack builderPack;
        private SelectionInfoColumnPropertyBuilderSprite builderSprite;
        private SelectionInfoColumnPropertyBuilderRoom builderRoom;
        private SelectionInfoColumnPropertyBuilderWeapon builderWeapon;
        private SelectionInfoColumnPropertyBuilderProjectile builderProjectile;
        private SelectionInfoColumnPropertyBuilderEnemy builderEnemy;
        private SelectionInfoColumnPropertyBuilderTile builderTile;

        private void Awake()
        {
            builderPack = new SelectionInfoColumnPropertyBuilderPack(ui.content, essentialsOnly);
            builderSprite = new SelectionInfoColumnPropertyBuilderSprite(ui.content);
            builderRoom = new SelectionInfoColumnPropertyBuilderRoom(ui.content);
            builderWeapon = new SelectionInfoColumnPropertyBuilderWeapon(ui.content);
            builderProjectile = new SelectionInfoColumnPropertyBuilderProjectile(ui.content);
            builderEnemy = new SelectionInfoColumnPropertyBuilderEnemy(ui.content);
            builderTile = new SelectionInfoColumnPropertyBuilderTile(ui.content);
            builders = new Dictionary<Type, Action<IAsset>>
            {
                { typeof(PackAsset), asset => {PrepareIcon(asset.Icon); builderPack.Build((PackAsset) asset); }},
                { typeof(CampaignAsset), asset => {PrepareIcon(asset.Icon);}},
                { typeof(PaletteAsset), asset => {PrepareIcon(asset.Icon); ui.content.ReleaseAllProperties();}},
                { typeof(SpriteAsset), asset => {PrepareIcon(asset.Icon); builderSprite.Build((SpriteAsset)asset);}},
                { typeof(WeaponAsset), asset => {PrepareIcon(asset.Icon); builderWeapon.Build((WeaponAsset)asset);}},
                { typeof(ProjectileAsset), asset => {PrepareIcon(asset.Icon); builderProjectile.Build((ProjectileAsset)asset);}},
                { typeof(EnemyAsset), asset => {PrepareIcon(asset.Icon); builderEnemy.Build((EnemyAsset)asset);}},
                { typeof(RoomAsset), asset => {PrepareBanner(((RoomAsset)asset).Banner); builderRoom.Build((RoomAsset)asset);}},
                { typeof(TileAsset), asset => {PrepareIcon(asset.Icon); builderTile.Build((TileAsset)asset);}},
            };
        }

        /// <summary>
        /// Prepare the Info Column for an asset.
        /// </summary>
        /// <param name="asset">The asset to prepare the column for.</param>
        public void Construct(IAssetWithDescription asset)
        {
            ui.description.text = asset.Description;
            ui.descriptionContainer.SetActive(true);
            Refresh(asset);
        }
        
        /// <summary>
        /// Prepare the Info Column for an asset.
        /// </summary>
        /// <param name="asset">The asset to prepare the column for.</param>
        public void Construct(IAsset asset)
        {
            if (ui.descriptionContainer != null) ui.descriptionContainer.SetActive(false);
            Refresh(asset);
        }
        
        /// <summary>
        /// Empties out the Info Column.
        /// </summary>
        public void ConstructEmpty(AssetType type)
        {
            ui.title.text = $"Select a {type.ToString().ToLower()}";
            if (ui.previewIconContainer != null) ui.previewIconContainer.SetActive(false);
            if (ui.previewBannerContainer != null) ui.previewBannerContainer.SetActive(false);
            ui.content.ReleaseAllProperties();
        }
        
        public IPWithValueBase<T> GetProperty<T>(int i)
        {
            Preconditions.IsIntLowerOrEqualTo(i, ui.content.childCount, nameof(i));
            Preconditions.IsIntInRange(i, 0, ui.content.childCount, nameof(i));
            return ui.content.GetChild(i).GetComponent<IPWithValueBase<T>>();
        }
        
        public void DisposeProperties()
        {
            ui.content.ReleaseAllProperties();
        }
        
        private void Refresh(IAsset asset)
        {
            ui.title.text = asset.Title;
            builders[asset.GetType()](asset);
        }
        
        private void PrepareIcon(Sprite sprite)
        {
            if (ui.previewBannerContainer != null) ui.previewBannerContainer.SetActive(false);
            ui.previewIcon.sprite = sprite;
            ui.previewIconContainer.SetActive(true);
        }
        
        private void PrepareBanner(Sprite sprite)
        {
            if (ui.previewIconContainer != null) ui.previewIconContainer.SetActive(false);
            ui.previewBanner.sprite = sprite;
            ui.previewBannerContainer.SetActive(true);
        }
        
        public bool IconShown { get => ui.previewIconContainer.activeSelf; }
        public bool BannerShown { get => ui.previewBannerContainer.activeSelf; }
        public int PropertiesCount { get => ui.content.childCount; }
        public string Title { get => ui.title.text; }
        public string Description { get => ui.description.text; }
        public Sprite Icon { get => ui.previewIcon.sprite; }
        public Sprite BannerIcon { get => ui.previewBanner.sprite; }
        
        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public TextMeshProUGUI description;
            public Image previewIcon;
            public Image previewBanner;
            public GameObject descriptionContainer;
            public GameObject previewIconContainer;
            public GameObject previewBannerContainer;
            public RectTransform content;
        }
    }
}