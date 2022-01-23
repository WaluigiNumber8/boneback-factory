using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Holds information about a specific asset.
    /// </summary>
    public class AssetSlot : InteractableHolderBase, IAssetHolder
    {
        [SerializeField] private UIInfo ui;

        private AssetType type;
        private AssetBase asset;
        
        public void Construct(AssetType type, int index, AssetBase asset, Image iconPos)
        {
            ui.iconImg = iconPos;
            Construct(type, index, asset);
        }
        public void Construct(AssetType type, int index, AssetBase asset)
        {
            this.type = type;
            this.index = index;
            this.asset = asset;
            ui.iconImg.sprite = asset.Icon;
        }

        public AssetType Type { get => type; }
        public AssetBase Asset { get => asset; }

        [System.Serializable]
        public struct UIInfo
        {
            public Image iconImg;
        }
    }
}