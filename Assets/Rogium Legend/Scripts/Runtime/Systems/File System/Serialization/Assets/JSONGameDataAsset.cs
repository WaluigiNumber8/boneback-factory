using System;
using RedRats.Systems.FileSystem;
using Rogium.Options.Core;
using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="GameDataAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONGameDataAsset : IEncodedObject<GameDataAsset>
    {
        public int ResolutionX, ResolutionY;
        public int ScreenMode;
        public bool VSync;

        public JSONGameDataAsset(GameDataAsset gameData)
        {
            ResolutionX = gameData.Resolution.x;
            ResolutionY = gameData.Resolution.y;
            ScreenMode = (int) gameData.ScreenMode;
            VSync = gameData.VSync;
        }
        
        public GameDataAsset Decode()
        {
            return new GameDataAsset(new Vector2Int(ResolutionX, ResolutionY), 
                                     (ScreenType) ScreenMode,
                                     VSync
                                     );
        }
    }
}