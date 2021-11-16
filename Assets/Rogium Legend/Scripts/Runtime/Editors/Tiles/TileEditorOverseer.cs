using System;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Editors.TileData
{
    /// <summary>
    /// Overseers everything happening in the Tile Editor.
    /// </summary>
    public class TileEditorOverseer : IEditorOverseer
    {
        public event Action<TileAsset, int> OnCompleteEditing;
        private TileAsset currentTile;
        private int myIndex;

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
        /// Assign an asset, that is going to be currently edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        public void AssignAsset(TileAsset asset, int index)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Room");
            currentTile = new TileAsset(asset);
            myIndex = index;
        }
        
        /// <summary>
        /// Updates the tile asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(TileAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentTile, "Currently active asset.");
            currentTile = new TileAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentTile, myIndex);
        }
        
        public TileAsset CurrentTile 
        {
            get 
            {
                if (currentTile == null) throw new MissingReferenceException("Current Tile has not been set. Did you forget to activate the editor?");
                return this.currentTile;
            } 
        }
    }
}