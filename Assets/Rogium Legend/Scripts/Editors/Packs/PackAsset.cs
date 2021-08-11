using System;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    public class PackAsset
    {
        public readonly string packName;
        public readonly string description;
        public readonly string author;
        public readonly Sprite icon;
        public readonly DateTime creationDateTime;

        public PackAsset(string packName, string description, string author, Sprite icon)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
            this.creationDateTime = DateTime.Now;
        }

        public PackAsset(string packName, string description, string author, Sprite icon, DateTime creationDateTime)
        {
            this.packName = packName;
            this.description = description;
            this.author = author;
            this.icon = icon;
            this.creationDateTime = creationDateTime;
        }

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset)obj;
            if (pack.packName == packName && pack.author == author && pack.creationDateTime == creationDateTime)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            string hash = packName + author + creationDateTime;
            return hash.GetHashCode();
        }
    }
}

