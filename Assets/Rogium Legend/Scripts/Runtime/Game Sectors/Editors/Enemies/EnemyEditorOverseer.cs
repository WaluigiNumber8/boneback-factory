using System;
using RedRats.Safety;
using UnityEngine;

namespace Rogium.Editors.Enemies
{
    /// <summary>
    /// Overseers everything happening in the Weapon Editor.
    /// </summary>
    public class EnemyEditorOverseer : IEditorOverseer
    {
        public event Action<EnemyAsset> OnAssignAsset; 
        public event Action<EnemyAsset> OnCompleteEditing;
        
        private EnemyAsset currentAsset;

        #region Singleton Pattern
        private static EnemyEditorOverseer instance;
        private static readonly object padlock = new object();
        public static EnemyEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new EnemyEditorOverseer();
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
        public void AssignAsset(EnemyAsset asset, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Tile");
            
            currentAsset = new EnemyAsset(asset);

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
            currentAsset = new EnemyAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset);
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