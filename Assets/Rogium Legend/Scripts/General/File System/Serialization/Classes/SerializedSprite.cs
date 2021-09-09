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

        /// <summary>
        /// Deserializes this serialized sprite and returns in the sprite format.
        /// </summary>
        /// <returns>A Sprite that Unity can use.</returns>
        public Sprite Deserialize()
        {
            Texture2D texture = new Texture2D(this.textureWidth, this.textureHeight);
            ImageConversion.LoadImage(texture, this.textureBytes);
            Sprite sprite = Sprite.Create(texture,
                                          new Rect(this.x, this.y, this.width, this.height),
                                          new Vector2(this.pivotX, this.pivotY));

            return sprite;
        }

    }
}