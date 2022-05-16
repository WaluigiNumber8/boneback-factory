using RedRats.Safety;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private Color[] previousColors;

        public PalettePicker() => palettes = new List<PaletteAsset>();

        /// <summary>
        /// Grabs colors from a palette based on an entered ID.
        /// If ID is empty or missing returns first found palette colors.
        /// </summary>
        /// <param name="paletteID">The ID of the palette to search for.</param>
        /// <returns>An array of colors form found the palette.</returns>
        public Color[] GrabBasedOn(string paletteID)
        {
            UpdateList();

            //TODO Fill with a default palette if no palettes are created the user chose to import the default pack.
            SafetyNet.EnsureListIsNotNullOrEmpty(palettes, "List of Pack Palettes");

            //If ID is not set yet.
            if (string.IsNullOrEmpty(paletteID))
            {
                previousID = paletteID;
                previousColors = palettes[0].Colors;
                return previousColors;
            }

            //If ID is the same as the last grab.
            if (paletteID == previousID)
            {
                return previousColors;
            }

            //Search for the ID in the list.
            try
            {
                previousColors = palettes.First(palette => palette.ID == paletteID).Colors;
                previousID = paletteID;
                return previousColors;
            }
            //If no ID was found, replace with "Not Found" Palette.
            catch (ArgumentNullException)
            {
                //TODO Replace with "Not Found Palette"
                previousID = paletteID;
                previousColors = palettes[0].Colors;
                return previousColors;
            }
        }

        /// <summary>
        /// Updates the list of palettes.
        /// </summary>
        private void UpdateList()
        {
            palettes = PackEditorOverseer.Instance.CurrentPack.Palettes;
        }
    }
}