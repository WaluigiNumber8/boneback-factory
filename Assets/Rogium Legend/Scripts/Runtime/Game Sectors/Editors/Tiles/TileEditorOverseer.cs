using System;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Overseers everything happening in the Tile Editor.
    /// </summary>
    public class TileEditorOverseer : IEditorOverseer
    {
        public event Action<TileAsset> OnAssignAsset; 
        public event Action<TileAsset> OnCompleteEditing;
        
        private TileAsset currentAsset;

        #region Singleton Pattern
        private static TileEditorOverseer instance;
        private static readonly object padlock = new object();
        public static TileEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new TileEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(TileAsset asset, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Tile");
            
            currentAsset = new TileAsset(asset);

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the tile asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(TileAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new TileAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset);
        }
        
        public TileAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Tile has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}