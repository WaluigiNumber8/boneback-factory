using System;
using RedRats.Systems.FileSystem.Serialization;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PaletteAsset"/>.
    /// </summary>
    [Serializable]
    public class SerializedPaletteAsset : SerializedAssetBase<PaletteAsset>
    {
        public readonly SerializedArray<Color, SerializedColor> colors;

        public SerializedPaletteAsset(PaletteAsset asset) : base(asset)
        {
            colors = new SerializedArray<Color, SerializedColor>(asset.Colors, c => new SerializedColor(c), c => c.Deserialize());
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public override PaletteAsset Deserialize()
        {
            Color[] deserializedColors = colors.Deserialize();

            return new PaletteAsset(id,
                                    title,
                                    icon.Deserialize(),
                                    author,
                                    deserializedColors,
                                    DateTime.Parse(creationDate));
        }
    }
}