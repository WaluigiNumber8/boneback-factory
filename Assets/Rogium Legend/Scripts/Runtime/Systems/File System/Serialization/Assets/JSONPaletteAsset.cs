using System;
using System.Collections.Generic;
using System.Linq;
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
        public string[] associatedAssetIDs;

        public JSONPaletteAsset(PaletteAsset asset) : base(asset)
        {
            colors = new JSONArray<Color, JSONColor>(asset.Colors, c => new JSONColor(c));
            associatedAssetIDs = asset.AssociatedAssetsIDs.ToArray();
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public override PaletteAsset Decode()
        {
            colors.SetDecodingMethod(c => c.Decode());
            Color[] deserializedColors = colors.Decode();

            return new PaletteAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithColors(deserializedColors)
                .WithAssociatedAssetIDs(associatedAssetIDs?.ToHashSet() ?? new HashSet<string>())
                .Build();
        }
    }
}