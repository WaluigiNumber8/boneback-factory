using BoubakProductions.Safety;
using Rogium.Editors.Sprites;
using System;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the Sprite Editor.
    /// </summary>
    public class SpriteEditorOverseer : IEditorOverseer
    {
        public event Action<SpriteAsset> OnAssignAsset;
        public event Action OnCompleteEditingBefore, OnCompleteEditingAfter;
        public event Action<SpriteAsset, int> OnCompleteEditing;

        private SpriteAsset currentAsset;
        private int myIndex;
        
        #region Singleton Pattern
        private static SpriteEditorOverseer instance;
        private static readonly object padlock = new object();
        public static SpriteEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new SpriteEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private SpriteEditorOverseer() { }
        
        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        public void AssignAsset(SpriteAsset asset, int index)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Sprite");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");
            
            currentAsset = asset;
            myIndex = index;
            
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the sprite asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset containing new data.</param>
        public void UpdateAsset(SpriteAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new SpriteAsset(updatedAsset);
        }

        public void CompleteEditing()
        {
            OnCompleteEditingBefore?.Invoke();
            OnCompleteEditing?.Invoke(currentAsset, myIndex);
            OnCompleteEditingAfter?.Invoke();
        }
        
        public SpriteAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Sprite has not been set. Did you forget to activate the editor?");
                return this.currentAsset;
            } 
        }
        
    }
}