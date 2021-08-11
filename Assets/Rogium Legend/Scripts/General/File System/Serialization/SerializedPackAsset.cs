using RogiumLegend.Editors.PackData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedPackAsset
    {
        public readonly string packName;
        public readonly string description;
        public readonly string author;
        public readonly SerializedSprite icon;
        public readonly string creationDateTime;

        public SerializedPackAsset(PackAsset packAsset)
        {
            this.packName = packAsset.packName;
            this.description = packAsset.description;
            this.author = packAsset.author;
            this.icon = SerializationFuncs.SerializeSprite(packAsset.icon);
            this.creationDateTime = packAsset.creationDateTime.ToString();
        }
    }
}