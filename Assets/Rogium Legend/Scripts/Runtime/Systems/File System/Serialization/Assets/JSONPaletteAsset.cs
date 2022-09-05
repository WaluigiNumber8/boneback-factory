using System;
using RedRats.Systems.FileSystem.JSON.Serialization;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PaletteAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONPaletteAsset : JSONAssetBase<PaletteAsset>
    {
        public JSONArray<Color, JSONColor> colors;

        public JSONPaletteAsset(PaletteAsset asset) : base(asset)
        {
            colors = new JSONArray<Color, JSONColor>(asset.Colors, c => new JSONColor(c));
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public override PaletteAsset Decode()
        {
            colors.SetDecodingMethod(c => c.Decode());
            Color[] deserializedColors = colors.Decode();

            return new PaletteAsset(id,
                                    title,
                                    icon.Decode(),
                                    author,
                                    deserializedColors,
                                    DateTime.Parse(creationDate));
        }
    }
}