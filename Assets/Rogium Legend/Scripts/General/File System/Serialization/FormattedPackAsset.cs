using RogiumLegend.Editors.PackData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    [System.Serializable]
    public class FormattedPackAsset
    {
        public string packName;
        public string description;
        public string author;
        public SerializedSprite icon;

        public FormattedPackAsset(PackAsset packAsset)
        {
            this.packName = packAsset.PackName;
            this.description = packAsset.Description;
            this.author = packAsset.Author;
            this.icon = SerializationFuncs.SerializeSprite(packAsset.Icon);
        }
    }
}