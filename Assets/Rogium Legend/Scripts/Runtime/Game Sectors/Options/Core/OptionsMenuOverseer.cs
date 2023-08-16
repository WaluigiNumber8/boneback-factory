using System;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the currently edited <see cref="GameDataAsset"/>.
    /// </summary>
    public class OptionsMenuOverseer : Singleton<OptionsMenuOverseer>, IEditorOverseer
    {
        public event Action<GameDataAsset> OnAssignAsset;
        public event Action<GameDataAsset> OnSaveChanges;

        private GameDataAsset currentAsset;

        /// <summary>
        /// Assign a new Preferences file to edit.
        /// </summary>
        /// <param name="asset">The preferences to edit.</param>
        public void AssignAsset(GameDataAsset asset)
        {
            SafetyNet.EnsureIsNotNull(asset, "Preferences Asset");
            currentAsset = new GameDataAsset(asset);
            
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        public void CompleteEditing()
        {
            OnSaveChanges?.Invoke(currentAsset);
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