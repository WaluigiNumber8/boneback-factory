using Rogium.Editors.Core;
using System;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.PaletteData
{
    public class PaletteAsset : AssetBase
    {
        private Color[] colors;

        #region Constrcutors
        public PaletteAsset()
        {
            GenerateID(EditorAssetIDs.PaletteIdentifier);
        }
        #endregion
        
        #region Update Values
        
        #endregion
    }
}
