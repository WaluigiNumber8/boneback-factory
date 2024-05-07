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
        public float MasterVolume, MusicVolume, SoundVolume, UIVolume;
        public int ResolutionX, ResolutionY;
        public int ScreenMode;
        public bool VSync;

        public JSONGameDataAsset(GameDataAsset gameData)
        {
            MasterVolume = gameData.MasterVolume;
            MusicVolume = gameData.MusicVolume;
            SoundVolume = gameData.SoundVolume;
            UIVolume = gameData.UIVolume;
            
            ResolutionX = gameData.Resolution.x;
            ResolutionY = gameData.Resolution.y;
            ScreenMode = (int) gameData.ScreenMode;
            VSync = gameData.VSync;
        }
        
        public GameDataAsset Decode()
        {
            return new GameDataAsset(MasterVolume,
                                     MusicVolume,
                                     SoundVolume,
                                     UIVolume,
                                     new Vector2Int(ResolutionX, ResolutionY), 
                                     (ScreenType) ScreenMode,
                                     VSync
                                     );
        }
    }
}