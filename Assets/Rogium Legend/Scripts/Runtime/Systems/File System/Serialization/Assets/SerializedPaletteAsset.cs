using System;
using BoubakProductions.Systems.Serialization;
using Rogium.Editors.PaletteData;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PaletteAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedPaletteAsset
    {
        public readonly string id;
        public readonly string title;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;

        public readonly SerializedArray<Color, SerializedColor> colors;

        public SerializedPaletteAsset(PaletteAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            this.creationDate = asset.CreationDate.ToString();

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