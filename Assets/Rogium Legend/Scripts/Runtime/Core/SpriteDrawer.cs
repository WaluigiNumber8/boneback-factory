using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using BoubakProductions.Safety;
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
        
        public SpriteDrawer(Vector2Int size, Vector2Int unitSize, int pixelsPerUnit)
        {
            this.size = size;
            this.unitSize = unitSize;
            this.pixelsPerUnit = pixelsPerUnit;

            int width = unitSize.x * size.x;
            int height = unitSize.y * size.y;
            emptyPixels = new Color[width * height];
            for (int index = 0; index < emptyPixels.Length; index++)
            {
                emptyPixels[index] = EditorDefaults.NoColor;
            }
        }

        /// <summary>
        /// Loads a sprite based on color data.
        /// </summary>
        /// <param name="indexGrid">The grid of IDs to read.</param>
        /// <param name="colorArray">The array of colors to take data from.</param>
        public Sprite Build(ObjectGrid<int> indexGrid, Color[] colorArray)
        {
            Texture2D tex = BoubakBuilder.GenerateTexture(indexGrid.Width * pixelsPerUnit, indexGrid.Height * pixelsPerUnit);
            Sprite sprite = BoubakBuilder.GenerateSprite(tex, pixelsPerUnit);
            ClearAllCells(sprite);
            
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    int id = indexGrid.GetValue(x, y);
                    
                    if (id == EditorDefaults.EmptyColorID) continue;

                    try
                    {
                        UpdateValue(sprite, new Vector2Int(x, y), colorArray[id]);
                    }
                    catch (InvalidOperationException)
                    {
                        UpdateValue(sprite, new Vector2Int(x, y), EditorDefaults.MissingColor);
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
        public Sprite Build<T>(ObjectGrid<string> IDGrid, IList<T> assetList) where T : IAsset
        {
            Texture2D tex = BoubakBuilder.GenerateTexture(IDGrid.Width * pixelsPerUnit, IDGrid.Height * pixelsPerUnit);
            Sprite sprite = BoubakBuilder.GenerateSprite(tex, pixelsPerUnit);
            ClearAllCells(sprite);
            
            AssetUtils.UpdateFromGridByList(IDGrid, assetList, 
                (x, y, asset) => UpdateValue(sprite, new Vector2Int(x, y), asset.Icon),
                (x, y) => UpdateValue(sprite, new Vector2Int(x, y), EditorDefaults.MissingSprite),
                (x, y, asset) => UpdateValue(sprite, new Vector2Int(x, y), EditorDefaults.EmptyGridSprite));
            
            Apply(sprite);
            return sprite;
        }
        
        /// <summary>
        /// Adds a new sprite onto teh edited one.
        /// </summary>
        /// <param name="sprite">The sprite to draw on.</param>
        /// <param name="pos">The position (in units) to draw at.</param>
        /// <param name="value">The color to draw.</param>
        public void UpdateValue(Sprite sprite, Vector2Int pos, Color value)
        {
            Sprite colorValue = BoubakBuilder.GenerateSprite(value, unitSize.x, unitSize.y, pixelsPerUnit);
            UpdateValue(sprite, pos, colorValue);
        }
        
        /// <summary>
        /// Draws a new 16x16 sprite on a layer.
        /// </summary>
        /// <param name="sprite">The sprite to draw on.</param>
        /// <param name="pos">The position (in units) to draw at.</param>
        /// <param name="value">The sprite to draw (MUST be same size as unit).</param>
        public void UpdateValue(Sprite sprite, Vector2Int pos, Sprite value)
        {
            Texture2D layer = sprite.texture;
            Texture2D tex = value.texture;
            SafetyNet.EnsureIntIsEqual(tex.width, 16, "Sprite Width");
            SafetyNet.EnsureIntIsEqual(tex.height, 16, "Sprite Height");

            int startX = pos.x * unitSize.x;
            int startY = pos.y * unitSize.y;

            layer.SetPixels(startX, startY, tex.width, tex.height, tex.GetPixels());
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
        private void ClearAllCells(Sprite sprite)
        {
            sprite.texture.SetPixels(emptyPixels);
        }
    }
}