using System;
using RedRats.Core;
using RedRats.Safety;
using UnityEngine;

namespace Rogium.Editors.Enemies
{
    /// <summary>
    /// Overseers everything happening in the Weapon Editor.
    /// </summary>
    public sealed class EnemyEditorOverseer : Singleton<EnemyEditorOverseer>, IEditorOverseer
    {
        public event Action<EnemyAsset> OnAssignAsset; 
        public event Action<EnemyAsset, int, string> OnCompleteEditing;
        
        private EnemyAsset currentAsset;
        private int myIndex;
        private string lastAssociatedSpriteID;

        private EnemyEditorOverseer() {}
        
        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(EnemyAsset asset, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Tile");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");

            currentAsset = new EnemyAsset.Builder().AsCopy(asset).Build();
            myIndex = index;
            lastAssociatedSpriteID = asset.AssociatedSpriteID;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(EnemyAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new EnemyAsset.Builder().AsCopy(updatedAsset).Build();
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex, lastAssociatedSpriteID);
        }
        
        public EnemyAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Enemy has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}