using RedRats.Systems.FileSystem.Serialization;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using System.Collections.Generic;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset : ISerializedObject<PackAsset>
    {
        private SerializedPackInfoAsset packInfo;

        public SerializedPackAsset(PackAsset asset)
        {
            packInfo = new SerializedPackInfoAsset(asset.PackInfo);
        }

        public PackAsset Deserialize()
        {
            return new PackAsset(packInfo.Deserialize());
        }
    }
}