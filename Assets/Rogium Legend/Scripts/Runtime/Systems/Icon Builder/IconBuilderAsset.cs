using BoubakProductions.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.IconBuilders
{
    /// <summary>
    /// Builds big icons based on asset icons.
    /// </summary>
    public class IconBuilderAsset
    {
        private string lastID;
        private Sprite lastSprite;

        /// <summary>
        /// Takes a list of asset data and lists their icons into a list of sprites.
        /// </summary>
        /// <param name="grid">The data to build from.</param>
        /// <param name="pixelsPerSprite">How many pixels to give to each sprite.</param>
        /// <param name="assets">The assets to use.</param>
        /// <returns>A Sprite.</returns>
        public Sprite Build<T>(ObjectGrid<string> grid, int pixelsPerSprite, AssetList<T> assets) where T : AssetBase
        {
            Texture2D tex = BoubakBuilder.GenerateTexture(grid.Width * pixelsPerSprite, grid.Height * pixelsPerSprite);
            int texPosX = 0;
            int texPosY = 0;
            
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    string id = grid.GetValue(x, y);
                    Sprite icon = GrabSprite(id, assets);
                    // Color[] colors = icon.texture.GetPixels();
                    CopyTextureTo(icon.texture, tex, texPosX, texPosY, texPosX + pixelsPerSprite, texPosY + pixelsPerSprite);
                    
                    // tex.SetPixels(texPosX, texPosY, texPosX+pixelsPerSprite-1, texPosY+pixelsPerSprite-1, colors);
                    texPosX += pixelsPerSprite;
                }

                texPosY += pixelsPerSprite;
            }

            return BoubakBuilder.GenerateSprite(tex, pixelsPerSprite);
        }

        /// <summary>
        /// Grabs a sprite from an asset list.
        /// </summary>
        /// <param name="id">the ID of the asset, whose icon is needed.</param>
        /// <param name="assets">The list of assets to search.</param>
        /// <typeparam name="T">Any type of <see cref="AssetBase"/>.</typeparam>
        /// <returns>The found icon of the asset.</returns>
        private Sprite GrabSprite<T>(string id, AssetList<T> assets) where T : AssetBase
        {
            if (id == EditorDefaults.EmptyAssetID)
            {
                return EditorDefaults.EmptyGridSprite;
            }
            
            if (id == lastID)
            {
                return lastSprite;
            }

            try
            {
                lastSprite = assets.GetByID(id).Icon;
                lastID = id;
                return lastSprite;
            }
            catch
            {
                return EditorDefaults.MissingSprite;
            }
        }

        private void CopyTextureTo(Texture2D texToCopy, Texture2D targetTex, int startX, int startY, int width, int height)
        {
            for (int y = startY; y < height; y++)
            {
                for (int x = startX; x < width; x++)
                {
                    targetTex.SetPixel(x, y, texToCopy.GetPixel(x, y));
                }
            }
        }
    }
}