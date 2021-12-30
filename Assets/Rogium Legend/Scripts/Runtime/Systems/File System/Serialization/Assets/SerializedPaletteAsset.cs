using System;
using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PaletteAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedPaletteAsset : SerializedAssetBase
    {
        public readonly SerializedArray<Color, SerializedColor> colors;

        public SerializedPaletteAsset(PaletteAsset asset) : base(asset)
        {
            this.colors = new SerializedArray<Color, SerializedColor>(asset.Colors, color => new SerializedColor(color));
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public PaletteAsset Deserialize()
        {
            Color[] deserializedColors = colors.Deserialize(color => color.Deserialize());

            return new PaletteAsset(this.id,
                                    this.title,
                                    this.icon.Deserialize(),
                                    this.author,
                                    deserializedColors,
                                    DateTime.Parse(creationDate));
        }
    }
}