using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    [System.Serializable]
    public class PackAsset
    {
        public string packName;
        public string description;
        public string author;
        public Sprite icon;

        public PackAsset(string packName, string description, string author, Sprite icon)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
        }
    }
}

