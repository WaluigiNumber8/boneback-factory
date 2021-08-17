using RogiumLegend.Editors.RoomData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedTileAsset
    {
        public readonly int tileType;
        public readonly SerializedSprite sprite;
        public readonly SerializedColor color;
        public SerializedMatrix4x4 transform;

        public SerializedTileAsset()
        {
            this.tileType = 1;
            this.sprite = new SerializedSprite(Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f)));
            this.color = new SerializedColor(Color.magenta);
            this.transform = new SerializedMatrix4x4(Matrix4x4.identity);
        }

        public SerializedTileAsset(TileAsset tileAsset)
        {
            this.tileType = (int)tileAsset.Type;
            this.sprite = new SerializedSprite(tileAsset.Tile.sprite);
            this.color = new SerializedColor(tileAsset.Tile.color);
            this.transform = new SerializedMatrix4x4(tileAsset.Tile.transform);
        }

        public SerializedTileAsset(int tileType, SerializedSprite sprite, SerializedColor color, SerializedMatrix4x4 transform)
        {
            this.tileType = tileType;
            this.sprite = sprite;
            this.color = color;
            this.transform = transform;
        }
    }
}