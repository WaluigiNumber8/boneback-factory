using Rogium.Core;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all objects, that hold a reference to an asset.
    /// </summary>
    public interface IAssetHolder : IIDHolder, IIndexHolder
    {
        public AssetType Type { get; }
        public IAsset Asset { get; }
    }
}