using RedRats.Systems.FileSystem;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONPackAsset : IEncodedObject<PackAsset>
    {
        public JSONPackInfoAsset packInfo;

        public JSONPackAsset(PackAsset asset)
        {
            packInfo = new JSONPackInfoAsset(asset.PackInfo);
        }

        public PackAsset Decode()
        {
            return new PackAsset(packInfo.Decode());
        }
    }
}