using System.Collections.Generic;
using RedRats.Core;
using Rogium.ExternalStorage;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the Options Menu.
    /// </summary>
    public class OptionsMenuOverseerMono : MonoSingleton<OptionsMenuOverseerMono>
    {
        [SerializeField] private Transform graphicsColumn;

        private ExternalStorageOverseer externalStorage;
        private OptionsGraphicsPropertyBuilder graphicsPropertyBuilder;

        private GameDataAsset gameData;

        protected override void Awake()
        {
            base.Awake();
            externalStorage = ExternalStorageOverseer.Instance;
            LoadPreferences();
        }

        private void Start()
        {
            graphicsPropertyBuilder = new OptionsGraphicsPropertyBuilder(graphicsColumn);
            graphicsPropertyBuilder.Build(gameData);
        }

        /// <summary>
        /// Prepares the Options menu for the user. 
        /// </summary>
        public void Prepare()
        {
            LoadPreferences();
        }
        
        /// <summary>
        /// Saves the current preferences.
        /// </summary>
        public void Save()
        {
            externalStorage.Preferences.Save(gameData);
        }

        /// <summary>
        /// Loads preferences from external storage.
        /// <p>If no data was found, returns default preferences.</p>
        /// </summary>
        private void LoadPreferences()
        {
            IList<GameDataAsset> data = externalStorage.Preferences.LoadAll();
            gameData = (data == null || data.Count <= 0) ? new GameDataAsset() : data[0];
        }
        
        public GameDataAsset GameData { get => gameData; }
    }
}