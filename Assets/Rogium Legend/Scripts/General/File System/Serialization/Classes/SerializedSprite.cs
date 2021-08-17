using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedSprite
    {
        public readonly float x;
        public readonly float y;
        public readonly float width;
        public readonly float height;
        public readonly float pivotX;
        public readonly float pivotY;
        public readonly int textureWidth;
        public readonly int textureHeight;
        public readonly byte[] textureBytes;

        public SerializedSprite(Sprite sprite) : this(sprite.rect.x,
                                                      sprite.rect.y,
                                                      sprite.rect.width,
                                                      sprite.rect.height,
                                                      sprite.pivot.x,
                                                      sprite.pivot.y,
                                                      sprite.texture.width,
                                                      sprite.texture.height,
                                                      ImageConversion.EncodeToPNG(sprite.texture)) {}

        public SerializedSprite(float x, float y, float width, float height, float pivotX, float pivotY, int textureWidth, int textureHeight, byte[] textureBytes)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.pivotX = pivotX;
            this.pivotY = pivotY;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.textureBytes = textureBytes;
        }
    }
}