using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.Packs
{
    public class PackInfoAsset : AssetBase
    {
        private string description;

        #region Constructors
        public PackInfoAsset()
        {
            this.title = EditorDefaults.PackTitle;
            this.icon = EditorDefaults.PackIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            this.description = EditorDefaults.PackDescription;
            GenerateID(EditorAssetIDs.PackIdentifier);
        }

        public PackInfoAsset(PackInfoAsset packInfo)
        {
            this.id = packInfo.ID;
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
            GenerateID(EditorAssetIDs.PackIdentifier);
        }
        public PackInfoAsset(string id, string title, Sprite icon, string author, string description, DateTime creationDateTime)
        {
            SafetyNet.EnsureStringInRange(title, 4, 30, "name");
            SafetyNet.EnsureStringInRange(description, 0, 2000, "description");
            SafetyNet.EnsureStringInRange(author, 8, 20, "author");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDateTime;
            this.description = description;
        }
        #endregion
        
        #region Update Values
        public void UpdateDescription(string newDescription)
        {
            this.description = newDescription;
        }
        #endregion
        
        public string Description { get => description; }
    }
}