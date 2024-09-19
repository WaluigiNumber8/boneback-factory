using RedRats.Safety;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Selects a palette from the Asset list, based on entered sprite.
    /// </summary>
    public class PalettePicker
    {
        private IList<PaletteAsset> palettes;

        private string previousID;
        private PaletteAsset previousAsset;

        public PalettePicker() => palettes = new List<PaletteAsset>();

        /// <summary>
        /// Grabs colors from a palette based on an entered ID.
        /// If ID is empty or missing returns first found palette colors.
        /// </summary>
        /// <param name="paletteID">The ID of the palette to search for.</param>
        /// <returns>An array of colors form found the palette.</returns>
        public PaletteAsset GrabBasedOn(string paletteID)
        {
            UpdateList();

            //If ID is not set yet, return first palette.
            if (string.IsNullOrEmpty(paletteID))
            {
                previousID = paletteID;
                previousAsset = palettes[0];
                return previousAsset;
            }

            //If ID is empty, return default palette.
            if (paletteID == EditorDefaults.EmptyAssetID)
            {
                return new PaletteAsset.Builder()
                    .WithID(EditorDefaults.EmptyAssetID)
                    .WithColors(EditorDefaults.Instance.MissingPalette)
                    .Build();
            }

            //If ID is the same as the last grab.
            if (paletteID == previousID)
            {
                return previousAsset;
            }

            //Search for the ID in the list.
            try
            {
                previousAsset = palettes.First(palette => palette.ID == paletteID);
                previousID = paletteID;
                return previousAsset;
            }
            //If no ID was found, replace with "Not Found" Palette.
            catch (ArgumentNullException)
            {
                previousID = paletteID;
                previousAsset = palettes.First();
                SafetyNet.ThrowMessage("Assigned palette was not found. Replaced with first one found");
                return previousAsset;
            }
        }

        /// <summary>
        /// Updates the list of palettes.
        /// </summary>
        private void UpdateList() => palettes = PackEditorOverseer.Instance.CurrentPack.Palettes;
    }
}