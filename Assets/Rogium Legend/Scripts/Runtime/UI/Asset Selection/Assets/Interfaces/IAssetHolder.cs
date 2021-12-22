using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    public interface IAssetHolder
    {
        public int ID { get; }
        public AssetType Type { get; }
        public AssetBase Asset { get; }

        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        /// <param name="iconPosition">Image, on which the icon will be drawn.</param>
        public void Construct(AssetType type, int id, AssetBase asset, Image iconPos);
        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        public void Construct(AssetType type, int id, AssetBase asset);
    }
}