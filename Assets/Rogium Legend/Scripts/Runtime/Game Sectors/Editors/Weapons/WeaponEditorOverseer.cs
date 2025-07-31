using System;
using RedRats.Core;
using RedRats.Safety;
using UnityEngine;

namespace Rogium.Editors.Weapons
{
    /// <summary>
    /// Overseers everything happening in the Weapon Editor.
    /// </summary>
    public sealed class WeaponEditorOverseer : Singleton<WeaponEditorOverseer>, IEditorOverseer
    {
        public event Action<WeaponAsset> OnAssignAsset; 
        public event Action<WeaponAsset, int, string> OnCompleteEditing;
        
        private WeaponAsset currentAsset;
        private int myIndex;
        private string lastAssociatedSpriteID;

        private WeaponEditorOverseer() {}
        
        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(WeaponAsset asset, int index, bool prepareEditor = true)
        {
            Preconditions.IsNotNull(asset, "Assigned Tile");
            Preconditions.IsIntBiggerOrEqualTo(index, 0, "Assigned asset index");
            currentAsset = new WeaponAsset.Builder().AsCopy(asset).Build();
            myIndex = index;
            lastAssociatedSpriteID = asset.AssociatedSpriteID;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(WeaponAsset updatedAsset)
        { 
            Preconditions.IsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new WeaponAsset.Builder().AsCopy(updatedAsset).Build();
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex, lastAssociatedSpriteID);
        }
        
        public WeaponAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Weapon has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}