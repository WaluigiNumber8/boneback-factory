using Rogium.Core;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// A base for all objects, that hold a reference to an asset.
    /// </summary>
    public interface IAssetHolder : IIDHolder
    {
        public int Index { get; }
        public AssetType Type { get; }
        public AssetBase Asset { get; }
    }
}