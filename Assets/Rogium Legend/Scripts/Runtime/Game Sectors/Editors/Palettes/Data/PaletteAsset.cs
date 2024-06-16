using System;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Contains all data needed for a palette.
    /// </summary>
    public class PaletteAsset : AssetWithDirectSpriteBase
    {
        private readonly Color[] colors;

        #region Constructors
        public PaletteAsset()
        {
            InitBase(EditorConstants.PaletteTitle, Resources.Load<Sprite>(EditorConstants.EmptyGridSpritePath), EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.PaletteIdentifier);

            colors = RedRatBuilder.GenerateColorArray(EditorConstants.PaletteSize, Color.black);
        }
        public PaletteAsset(string title, Sprite icon)
        {
            InitBase(title, icon, EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.PaletteIdentifier);

            colors = RedRatBuilder.GenerateColorArray(EditorConstants.PaletteSize, Color.black);
        }
        public PaletteAsset(PaletteAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            id = asset.ID;
            InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);

            colors = asset.Colors;
        }
        
        public PaletteAsset(string id, string title, Sprite icon, string author, Color[] colors, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            InitBase(title, icon, author, creationDate);

            this.colors = colors;
        }
        #endregion
        
        #region Update Values
        #endregion
        
        public Color[] Colors { get => colors; }
    }
}
