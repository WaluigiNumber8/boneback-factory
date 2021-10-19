using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Rogium.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedSprite
    {
        public readonly float rectX;
        public readonly float rectY;
        public readonly float rectWidth;
        public readonly float rectHeight;
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

        public SerializedSprite(float rectX, float rectY, float rectWidth, float rectHeight, float pivotX, float pivotY, int textureWidth, int textureHeight, byte[] textureBytes)
        {
            this.rectX = rectX;
            this.rectY = rectY;
            this.rectWidth = rectWidth;
            this.rectHeight = rectHeight;
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
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(this.textureBytes);
            Sprite sprite = Sprite.Create(texture,
                                          new Rect(rectX, rectY, rectWidth, rectHeight),
                                          new Vector2(pivotX / rectWidth, pivotY / rectHeight),
                                          16);
            return sprite;
        }

    }
}