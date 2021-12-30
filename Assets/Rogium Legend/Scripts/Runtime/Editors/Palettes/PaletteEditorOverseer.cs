using System;
using BoubakProductions.Safety;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the Palette Editor.
    /// </summary>
    public class PaletteEditorOverseer : IEditorOverseer
    {
        public event Action<PaletteAsset> OnAssignAsset;
        public event Action<PaletteAsset, int> OnCompleteEditing;
        
        private PaletteAsset currentAsset;
        private int myIndex;
        
        #region Singleton Pattern
        private static PaletteEditorOverseer instance;
        private static readonly object padlock = new object();
        public static PaletteEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new PaletteEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        public void AssignAsset(PaletteAsset asset, int index)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Palette");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");
            
            currentAsset = asset;
            myIndex = index;
            
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the tile asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(PaletteAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new PaletteAsset(updatedAsset);
        }

        /// <summary>
        /// Updates a color on a specific position.
        /// </summary>
        /// <param name="color">The new color to assign.</param>
        /// <param name="posIndex">The position for the color.</param>
        public void UpdateColor(Color color, int posIndex)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(posIndex, 0, "posIndex");
            currentAsset.Colors[posIndex] = color;
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(currentAsset, myIndex);
        }
        
        public PaletteAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Palette has not been set. Did you forget to activate the editor?");
                return this.currentAsset;
            } 
        }
        
    }
}