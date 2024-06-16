using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Draws sprites onto other sprites.
    /// </summary>
    public class SpriteDrawer
    {
        private readonly Vector2Int size;
        private readonly Vector2Int unitSize;
        private readonly int pixelsPerUnit;
        
        private readonly Color[] emptyPixels;
        private readonly bool overdrawTransparent;
        
        public SpriteDrawer(Vector2Int size, Vector2Int unitSize, int pixelsPerUnit, bool overdrawTransparent = true)
        {
            this.size = size;
            this.unitSize = unitSize;
            this.pixelsPerUnit = pixelsPerUnit;
            this.overdrawTransparent = overdrawTransparent;

            int width = unitSize.x * size.x;
            int height = unitSize.y * size.y;
            emptyPixels = new Color[width * height];
            for (int index = 0; index < emptyPixels.Length; index++)
            {
                emptyPixels[index] = EditorConstants.NoColor;
            }
        }

        /// <summary>
        /// Loads a sprite based on color data.
        /// </summary>
        /// <param name="indexGrid">The grid of IDs to read.</param>
        /// <param name="colorArray">The array of colors to take data from.</param>
        public Sprite Build(ObjectGrid<int> indexGrid, Color[] colorArray)
        {
            Texture2D tex = RedRatBuilder.GenerateTexture(indexGrid.Width * pixelsPerUnit, indexGrid.Height * pixelsPerUnit);
            Sprite sprite = RedRatBuilder.GenerateSprite(tex, pixelsPerUnit);
            ClearAllCells(sprite);
            
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    int id = indexGrid.GetValue(x, y);
                    
                    if (id == EditorConstants.EmptyColorID) continue;

                    try
                    {
                        DrawTo(sprite, new Vector2Int(x, y), colorArray[id]);
                    }
                    catch (InvalidOperationException)
                    {
                        DrawTo(sprite, new Vector2Int(x, y), EditorConstants.MissingColor);
                    }
                }
            }
            
            Apply(sprite);
            return sprite;
        }

        /// <summary>
        /// Loads a sprite based on asset data.
        /// </summary>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <param name="assetList">The list of assets to take data from.</param>
        /// <typeparam name="T">Any type of <see cref="IAsset"/>.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>.</typeparam>
        public Sprite Build<T, TS>(ObjectGrid<TS> IDGrid, IList<T> assetList) where T : IAsset where TS : IComparable
        {
            Texture2D tex = RedRatBuilder.GenerateTexture(IDGrid.Width * pixelsPerUnit, IDGrid.Height * pixelsPerUnit);
            Sprite sprite = RedRatBuilder.GenerateSprite(tex, pixelsPerUnit);
            ClearAllCells(sprite);

            return Build(IDGrid, assetList, sprite);
        }

        /// <summary>
        /// Loads a sprite based on asset data.
        /// </summary>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <param name="assetList">The list of assets to take data from.</param>
        /// <param name="sprite">The sprite to draw onto.</param>
        /// <typeparam name="T">Any type of <see cref="IAsset"/>.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>.</typeparam>
        public Sprite Build<T, TS>(ObjectGrid<TS> IDGrid, IList<T> assetList, Sprite sprite) where T : IAsset where TS : IComparable
        {
            SafetyNet.EnsureIntIsEqual(sprite.texture.width, IDGrid.Width * unitSize.x, "Bottom Sprite Width");
            SafetyNet.EnsureIntIsEqual(sprite.texture.height, IDGrid.Height * unitSize.y, "Bottom Sprite Height");
            SafetyNet.EnsureFloatIsEqual(sprite.pixelsPerUnit, pixelsPerUnit, "Pixels per unit");
            
            AssetUtils.UpdateFromGridByList(IDGrid, assetList, 
                (x, y, asset) => DrawTo(sprite, new Vector2Int(x, y), asset.Icon),
                (x, y) => DrawTo(sprite, new Vector2Int(x, y), Resources.Load<Sprite>(EditorConstants.MissingSpritePath)));
            
            Apply(sprite);
            return sprite;
        }
        
        /// <summary>
        /// Adds a new sprite onto the edited one.
        /// </summary>
        /// <param name="canvas">The sprite to draw on.</param>
        /// <param name="pos">The position (in units) to draw at.</param>
        /// <param name="value">The color to draw.</param>
        public void DrawTo(Sprite canvas, Vector2Int pos, Color value)
        {
            Sprite colorValue = RedRatBuilder.GenerateSprite(value, unitSize.x, unitSize.y, pixelsPerUnit);
            DrawTo(canvas, pos, colorValue);
        }
        
        /// <summary>
        /// Draws a new 16x16 sprite on a layer.
        /// </summary>
        /// <param name="canvas">The sprite to draw on.</param>
        /// <param name="pos">The position (in units) to draw at.</param>
        /// <param name="sprite">The sprite to draw (MUST be same size as unit).</param>
        public void DrawTo(Sprite canvas, Vector2Int pos, Sprite sprite)
        {
            Texture2D canvasTex = canvas.texture;
            Texture2D tex = sprite.texture;
            SafetyNet.EnsureIntIsEqual(tex.width, 16, $"{tex.name}'s width");
            SafetyNet.EnsureIntIsEqual(tex.height, 16, $"{tex.name}'s height");

            int startX = pos.x * unitSize.x;
            int startY = pos.y * unitSize.y;
            
            Color[] texPixels = tex.GetPixels();
            Color[] layerPixels = canvasTex.GetPixels(startX, startY, tex.width, tex.height);

            for (int i = 0; i < texPixels.Length; i++)
            {
                if (!overdrawTransparent && texPixels[i].a == 0) continue; // if the pixel is not transparent
                layerPixels[i] = texPixels[i];
            }

            canvasTex.SetPixels(startX, startY, tex.width, tex.height, layerPixels);
        }

        /// <summary>
        /// Returns a section of the sprite based on the unitSize.
        /// </summary>
        /// <param name="canvas">The sprite to read from.</param>
        /// <param name="pos">The starting position to read from. Width and Height are unitSize.</param>
        public Sprite Get(Sprite canvas, Vector2Int pos)
        {
            Texture2D canvasTex = canvas.texture;
            
            int startX = pos.x * unitSize.x;
            int startY = pos.y * unitSize.y;
            Color[] spritePixels = canvasTex.GetPixels(startX, startY, unitSize.x, unitSize.y);
            
            Texture2D tex = RedRatBuilder.GenerateTexture(unitSize.x, unitSize.y);
            tex.SetPixels(spritePixels);
            tex.Apply();

            return RedRatBuilder.GenerateSprite(tex, pixelsPerUnit);
        }
        
        /// <summary>
        /// Refreshes the sprite's pixel changes (costly).
        /// </summary>
        /// <param name="sprite">The sprite to refresh.</param>
        public void Apply(Sprite sprite) => sprite.texture.Apply();
        
        /// <summary>
        /// Resets all cells to their empty state.
        /// </summary>
        /// <param name="sprite">The sprite to clear.</param>
        public void ClearAllCells(Sprite sprite) => sprite.texture.SetPixels(emptyPixels);
    }
}