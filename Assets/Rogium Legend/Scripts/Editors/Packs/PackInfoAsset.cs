using BoubakProductions.Safety;
using System;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    public class PackInfoAsset
    {
        private string title;
        private Sprite icon;
        private string author;
        private DateTime creationDate;
        private string description;

        public PackInfoAsset(string title, Sprite icon, string author, string description)
        {
            SafetyNet.EnsureStringInRange(title, 4, 30, "name");
            SafetyNet.EnsureStringInRange(description, 0, 2000, "description");
            SafetyNet.EnsureStringInRange(author, 8, 20, "author");

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.description = description;
        }
        public PackInfoAsset(string title, Sprite icon, string author, string description, DateTime creationDateTime)
        {
            SafetyNet.EnsureStringInRange(title, 4, 30, "name");
            SafetyNet.EnsureStringInRange(description, 0, 2000, "description");
            SafetyNet.EnsureStringInRange(author, 8, 20, "author");

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDateTime;
            this.description = description;
        }

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }
        public string Description { get => description; }
    }
}