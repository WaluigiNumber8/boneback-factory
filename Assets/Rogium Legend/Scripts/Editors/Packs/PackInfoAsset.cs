using System;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    public class PackInfoAsset
    {
        public readonly string packName;
        public readonly string description;
        public readonly string author;
        public readonly Sprite icon;
        public readonly DateTime creationDateTime;

        public PackInfoAsset(string packName, string description, string author, Sprite icon)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
            creationDateTime = DateTime.Now;
        }

        public PackInfoAsset(string packName, string description, string author, Sprite icon, DateTime creationDateTime)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
            this.creationDateTime = creationDateTime;
        }
    }
}