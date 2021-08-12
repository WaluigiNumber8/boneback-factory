using RogiumLegend.Editors.PackData;
using System;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{

    /// <summary>
    /// Stores methods, that will serialize special data types (like sprites).
    /// </summary>
    public static class SerializationFuncs
    {
        /// <summary>
        /// Converts a formatted pack asset into a normal pack asset.
        /// </summary>
        /// <param name="packAsset">Formatted pack asset to convert.</param>
        /// <returns>A normal pack asset.</returns>
        public static PackAsset DeserializePackAsset(SerializedPackAsset packAsset)
        {
            PackBuilder builder = new PackBuilder();
            PackAsset pack = builder.BuildPack(packAsset.packInfo.packName,
                                               packAsset.packInfo.description,
                                               packAsset.packInfo.author,
                                               DeserializeSprite(packAsset.packInfo.icon),
                                               DateTime.Parse(packAsset.packInfo.creationDateTime));
                              
            return pack;
        }

        /// <summary>
        /// Returns a serializable form of a sprite (Texture2D).
        /// </summary>
        /// <param name="sprite">The sprite to serialize.</param>
        /// <returns>The sprite in a Textture2D form.</returns>
        public static SerializedSprite SerializeSprite(Sprite sprite)
        {
            Texture2D texture = sprite.texture;
            SerializedSprite serializedSprite = new SerializedSprite(sprite.rect.x,
                                                                     sprite.rect.y,
                                                                     sprite.rect.width,
                                                                     sprite.rect.height,
                                                                     sprite.pivot.x,
                                                                     sprite.pivot.y,
                                                                     texture.width,
                                                                     texture.height,
                                                                     ImageConversion.EncodeToPNG(texture));
            return serializedSprite;
        }

        /// <summary>
        /// Deserializes a serialized sprite and returns in the sprite format.
        /// </summary>
        /// <param name="spr">Serialized Sprite to deserialize.</param>
        /// <returns>A Sprite that Unity can use.</returns>
        public static Sprite DeserializeSprite(SerializedSprite spr)
        {
            Texture2D texture = new Texture2D(spr.textureWidth, spr.textureHeight);
            ImageConversion.LoadImage(texture, spr.textureBytes);
            Sprite sprite = Sprite.Create(texture,
                                          new Rect(spr.x, spr.y, spr.width, spr.height),
                                          new Vector2(spr.pivotX, spr.pivotY));

            return sprite;
        }
    }
}