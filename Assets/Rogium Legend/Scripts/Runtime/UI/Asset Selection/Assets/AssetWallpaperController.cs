using System;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Contains References to an Asset Wallpaper, as well as the ability to be constructed with data.
    /// </summary>
    public class AssetWallpaperController : AssetHolderBase
    {
        public event Action<int> OnConstruct;
        
        [SerializeField] private Sprite emptyWallpaper;
        [SerializeField, Multiline(4)] private string emptyMessage;
        [SerializeField] private UIInfo ui;

        private AssetType type;
        private int id = -1;
        private AssetBase asset;
        
        public override void Construct(AssetType type, int index, AssetBase asset, Image wallpaperPos)
        {
            ui.wallpaper = wallpaperPos;
            Construct(type, index, asset);
        }
        public override void Construct(AssetType type, int index, AssetBase asset)
        {
            ui.emptyText.gameObject.SetActive(false);
            this.type = type;
            this.id = index;
            this.asset = asset;
            this.ui.title.text = asset.Title;
            this.ui.wallpaper.sprite = asset.Icon;
            
            OnConstruct?.Invoke(this.id);
        }

        /// <summary>
        /// Constructs the wallpaper, but without any campaign data.
        /// </summary>
        public void ConstructEmpty()
        {
            this.type = AssetType.None;
            this.id = -1;
            this.asset = null;
            this.ui.title.text = "";
            this.ui.wallpaper.sprite = emptyWallpaper;
            this.ui.emptyText.text = emptyMessage;
            this.ui.emptyText.gameObject.SetActive(true);
            
            OnConstruct?.Invoke(this.id);
        }
        
        public override int Index { get => id; }
        public override AssetType Type { get => type; }
        public override AssetBase Asset { get => asset; }

        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image wallpaper;
            public TextMeshProUGUI emptyText;
        }
    }
}