using RogiumLegend.ExternalStorage.Serialization;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    public class PackAsset
    {
        private readonly string packName;
        private readonly string description;
        private readonly string author;
        private readonly Sprite icon;

        public PackAsset(string packName, string description, string author, Sprite icon)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
        }

        public string PackName { get => packName; }
        public string Description { get => description; }
        public string Author { get => author; }
        public Sprite Icon { get => icon; }
    }
}

