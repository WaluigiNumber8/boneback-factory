using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedSprite
    {
        private readonly float x;
        private readonly float y;
        private readonly float width;
        private readonly float height;
        private readonly float pivotX;
        private readonly float pivotY;
        private readonly int textureWidth;
        private readonly int textureHeight;
        private readonly byte[] textureBytes;  

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

        public float Width { get => width; }
        public float Height { get => height; }
        public float X { get => x; }
        public float Y { get => y; }
        public float PivotX { get => pivotX; }
        public float PivotY { get => pivotY; }
        public int TextureWidth { get => textureWidth; }
        public int TextureHeight { get => textureHeight; }
        public byte[] TextureBytes { get => textureBytes; }
        
    }
}