using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    public abstract class AssetHolderBase : MonoBehaviour, IAssetHolder
    {
        public abstract int Index { get; }
        public abstract AssetType Type { get; }
        public abstract AssetBase Asset { get; }

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
    }
}