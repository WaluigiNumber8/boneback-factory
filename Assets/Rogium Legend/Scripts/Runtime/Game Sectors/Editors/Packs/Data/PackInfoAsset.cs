using System;
using UnityEngine;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;

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

        public PackInfoAsset(PackInfoAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            AssetValidation.ValidateDescription(asset.description);
            
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;
            this.description = asset.Description;
        }

        public PackInfoAsset(string title, Sprite icon, string author, string description)
        {
            AssetValidation.ValidateTitle(title);
            AssetValidation.ValidateDescription(description);

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.description = description;
            GenerateID(EditorAssetIDs.PackIdentifier);
        }
        public PackInfoAsset(string id, string title, Sprite icon, string author, string description, DateTime creationDateTime)
        {
            AssetValidation.ValidateTitle(title);
            AssetValidation.ValidateDescription(description);

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDateTime;
            this.description = description;
        }
        #endregion
        
        #region Update Values
        public void UpdateDescription(string newDescription) => description = newDescription;

        #endregion
        
        public string Description { get => description; }
    }
}