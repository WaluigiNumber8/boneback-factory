using Rogium.Core;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.AssetSelection
{
    public interface IAssetHolder
    {
        public int Index { get; }
        public AssetType Type { get; }
        public AssetBase Asset { get; }
    }
}