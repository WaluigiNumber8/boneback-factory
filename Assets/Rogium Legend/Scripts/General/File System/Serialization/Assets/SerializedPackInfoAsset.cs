using RogiumLegend.Editors.PackData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackInfoAsset, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackInfoAsset
    {
        public readonly string packName;
        public readonly string description;
        public readonly string author;
        public readonly SerializedSprite icon;
        public readonly string creationDateTime;

        public SerializedPackInfoAsset(PackInfoAsset packInfo)
        {
            this.packName = packInfo.packName;
            this.description = packInfo.description;
            this.author = packInfo.author;
            this.icon = SerializationFuncs.SerializeSprite(packInfo.icon);
            this.creationDateTime = packInfo.creationDateTime.ToString();
        }

    }
}