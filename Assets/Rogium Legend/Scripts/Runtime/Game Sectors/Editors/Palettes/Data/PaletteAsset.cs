using System;
using BoubakProductions.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Contains all data needed for a palette.
    /// </summary>
    public class PaletteAsset : AssetBase
    {
        private Color[] colors;

        #region Constructors
        public PaletteAsset()
        {
            this.title = EditorDefaults.PaletteTitle;
            this.icon = EditorDefaults.EmptyGridSprite;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.PaletteIdentifier);

            this.colors = BoubakBuilder.GenerateColorArray(EditorDefaults.PaletteSize, Color.black);
        }

        public PaletteAsset(PaletteAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.creationDate;

            this.colors = asset.Colors;
        }
        
        public PaletteAsset(string id, string title, Sprite icon, string author, Color[] colors, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.colors = colors;
        }
        #endregion
        
        #region Update Values
        public void UpdateColors(Color[] newColors) => colors = newColors;
        #endregion
        
        public Color[] Colors { get => colors; }
    }
}
