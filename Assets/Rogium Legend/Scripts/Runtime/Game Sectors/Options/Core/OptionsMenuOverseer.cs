using System;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the currently edited <see cref="PreferencesAsset"/>.
    /// </summary>
    public sealed class OptionsMenuOverseer : Singleton<OptionsMenuOverseer>, IEditorOverseer
    {
        public event Action<GameDataAsset> OnAssignAsset;
        public event Action<GameDataAsset> OnApplySettings;
        public event Action<GameDataAsset> OnSaveChanges;

        private GameDataAsset currentAsset;

        /// <summary>
        /// Assign a new Preferences file to edit.
        /// </summary>
        /// <param name="asset">The preferences to edit.</param>
        /// <param name="prepareEditor">If TRUE, load asset into the editor.</param>
        public void AssignAsset(GameDataAsset asset, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Preferences Asset");
            currentAsset = new GameDataAsset.Builder().AsCopy(asset).Build();
            InputSystem.GetInstance().ClearAllInput();
            ShortcutToAssetConverter.Load(asset.ShortcutBindings);
            InputToAssetConverter.Load(asset.InputBindings);
            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(CurrentAsset);
        }

        /// <summary>
        /// Apply all settings to the game for a specific asset.
        /// </summary>
        public void ApplyAllOptions(GameDataAsset gameData) => OnApplySettings?.Invoke(gameData);
        
        public void CompleteEditing()
        {
            currentAsset.UpdateInputBindings(InputToAssetConverter.Get());
            currentAsset.UpdateShortcutBindings(ShortcutToAssetConverter.Get());
            OnSaveChanges?.Invoke(CurrentAsset);
            
            InputSystem.GetInstance().RemoveAllEmptyBindings();
        }
        
        public GameDataAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Preferences has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}