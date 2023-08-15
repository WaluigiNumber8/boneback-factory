using System;
using RedRats.Systems.FileSystem;
using Rogium.Options.Core;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="GameDataAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONGameDataAsset : IEncodedObject<GameDataAsset>
    {
        public int ScreenMode;

        public JSONGameDataAsset(GameDataAsset gameData)
        {
            ScreenMode = (int) gameData.ScreenMode;
        }
        
        public GameDataAsset Decode()
        {
            return new GameDataAsset((FullScreenMode) ScreenMode);
        }
    }
}