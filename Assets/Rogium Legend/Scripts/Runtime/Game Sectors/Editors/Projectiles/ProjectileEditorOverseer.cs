using System;
using RedRats.Safety;
using UnityEngine;

namespace Rogium.Editors.Projectiles
{
    /// <summary>
    /// Overseers everything happening in the Projectile Editor.
    /// </summary>
    public class ProjectileEditorOverseer : IEditorOverseer
    {
        public event Action<ProjectileAsset> OnAssignAsset; 
        public event Action<ProjectileAsset> OnCompleteEditing;
        
        private ProjectileAsset currentAsset;

        #region Singleton Pattern
        private static ProjectileEditorOverseer instance;
        private static readonly object padlock = new object();
        public static ProjectileEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ProjectileEditorOverseer();
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
        public void AssignAsset(ProjectileAsset asset, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Tile");
            
            currentAsset = new ProjectileAsset(asset);

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(ProjectileAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new ProjectileAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentAsset);
        }
        
        public ProjectileAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Projectile has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}