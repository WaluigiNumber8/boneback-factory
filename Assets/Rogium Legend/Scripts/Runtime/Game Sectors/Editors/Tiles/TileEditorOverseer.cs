using System;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Overseers everything happening in the Tile Editor.
    /// </summary>
    public sealed class TileEditorOverseer : Singleton<TileEditorOverseer>, IEditorOverseer
    {
        public event Action<TileAsset> OnAssignAsset; 
        public event Action<TileAsset, int, string> OnCompleteEditing;
        
        private TileAsset currentAsset;
        private int myIndex;
        private string lastAssociatedSpriteID;

        private TileEditorOverseer() {}
        
        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(TileAsset asset, int index, bool prepareEditor = true)
        {
            Preconditions.IsNotNull(asset, "Assigned Tile");
            Preconditions.IsIntBiggerOrEqualTo(index, 0, "Assigned asset index");

            currentAsset = new TileAsset.Builder().AsCopy(asset).Build();
            myIndex = index;
            lastAssociatedSpriteID = asset.AssociatedSpriteID;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the tile asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(TileAsset updatedAsset)
        { 
            Preconditions.IsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new TileAsset.Builder().AsCopy(updatedAsset).Build();
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex, lastAssociatedSpriteID);
        }
        
        public TileAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Tile has not been set. Did you forget to activate the editor?");
                return this.currentAsset;
            } 
        }
    }
}