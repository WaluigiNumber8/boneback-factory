using BoubakProductions.Systems.Serialization;
using Rogium.Editors.TileData;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedTileAsset
    {
        public readonly string id;
        public readonly string title;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;
        public readonly int tileType;
        public readonly SerializedColor color;

        public SerializedTileAsset(TileAsset tileAsset)
        {
            this.id = tileAsset.ID;
            this.title = tileAsset.Title;
            this.author = tileAsset.Author;
            this.creationDate = tileAsset.CreationDate.ToString();
            this.tileType = (int)tileAsset.Type;
            this.icon = new SerializedSprite(tileAsset.Tile.sprite);
            this.color = new SerializedColor(tileAsset.Tile.color);
        }

        /// <summary>
        /// Turns the Tile Asset into a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public TileAsset Deserialize()
        {
            return new TileAsset(this.id,
                                 this.title,
                                 this.icon.Deserialize(),
                                 this.author,
                                 (TileType)this.tileType,
                                 this.color.Deserialize(),
                                 DateTime.Parse(this.creationDate));
        }
    }
}