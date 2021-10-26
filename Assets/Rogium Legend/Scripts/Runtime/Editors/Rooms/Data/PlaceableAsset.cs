using Rogium.Editors.Core;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// The asset that is selected via the <see cref="AssetPickerOverseer"/>.
    /// </summary>
    public class PlaceableAsset
    {
        private AssetBase asset;
        private PlaceableAssetType type;

        public PlaceableAsset(AssetBase asset, PlaceableAssetType type)
        {
            this.asset = asset;
            this.type = type;
        }

        public AssetBase Asset => asset;
        public PlaceableAssetType Type => type;
    }
}