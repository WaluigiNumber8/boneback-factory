using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// A base for all classes holding information about assets and being controlled via an internal toggle.
    /// </summary>
    public abstract class AssetHolderBase : ToggleableIndexBase, IAssetHolder
    {
        protected AssetType type;
        protected AssetBase asset;

        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="index">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        /// <param name="iconPos">Image, on which the icon will be drawn.</param>
        public abstract void Construct(AssetType type, int index, AssetBase asset, Image iconPos);
        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="index">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        public abstract void Construct(AssetType type, int index, AssetBase asset);
        public string ID { get => asset.ID; }
        
        public AssetType Type {get => type;}
        public AssetBase Asset {get => asset;}
    }
}