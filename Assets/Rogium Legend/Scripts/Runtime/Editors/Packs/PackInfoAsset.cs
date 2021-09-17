using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.PackData
{
    public class PackInfoAsset : IAsset
    {
        private string title;
        private Sprite icon;
        private string author;
        private DateTime creationDate;
        private string description;

        #region Constructors

        public PackInfoAsset() 
        {
            this.title = EditorDefaults.packTitle;
            this.icon = EditorDefaults.packIcon;
            this.author = EditorDefaults.author;
            this.creationDate = DateTime.Now;
            this.description = EditorDefaults.packDescription;
        }

        public PackInfoAsset(PackInfoAsset packInfo)
        {
            this.title = packInfo.Title;
            this.icon = packInfo.Icon;
            this.author = packInfo.Author;
            this.creationDate = packInfo.CreationDate;
            this.description = packInfo.Description;
        }

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
        #endregion

        #region Update Values
        public void UpdateTitle(string newTitle)
        {
            this.title = newTitle;
        }

        public void UpdateIcon(Sprite newIcon)
        {
            this.icon = newIcon;
        }

        public void UpdateAuthor(string newAuthor)
        {
            this.author = newAuthor;
        }

        public void UpdateCreationDate(DateTime newCreationDate)
        {
            this.creationDate = newCreationDate;
        }

        public void UpdateDescription(string newDescription)
        {
            this.description = newDescription;
        }
        #endregion

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }
        public string Description { get => description; }
    }
}