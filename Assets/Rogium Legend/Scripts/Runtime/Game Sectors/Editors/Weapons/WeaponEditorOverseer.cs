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
        public event Action<WeaponAsset, int> OnCompleteEditing;
        
        private WeaponAsset currentAsset;
        private int myIndex;

        private WeaponEditorOverseer() {}
        
        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(WeaponAsset asset, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Tile");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");
            currentAsset = new WeaponAsset(asset);
            myIndex = index;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(WeaponAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new WeaponAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex);
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