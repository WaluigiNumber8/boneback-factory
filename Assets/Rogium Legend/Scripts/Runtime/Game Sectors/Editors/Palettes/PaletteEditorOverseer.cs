using System;
using RedRats.Safety;
using Rogium.Systems.IconBuilders;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the Palette Editor.
    /// </summary>
    public class PaletteEditorOverseer : IEditorOverseer
    {
        public event Action<PaletteAsset> OnAssignAsset;
        public event Action OnCompleteEditingBefore, OnCompleteEditingAfter;
        public event Action<PaletteAsset> OnCompleteEditing;

        private readonly IconBuilder iconBuilder;
        
        private PaletteAsset currentAsset;
        
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

        private PaletteEditorOverseer() => iconBuilder = new IconBuilder();

        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(PaletteAsset asset, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Palette");
            
            currentAsset = new PaletteAsset(asset);

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the Palette Asset with new data. Not allowed when no asset is assigned.
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
            currentAsset.UpdateColorSlot(posIndex, color);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditingBefore?.Invoke();
            
            currentAsset.UpdateIcon(iconBuilder.BuildFromArray(currentAsset.Colors));
            OnCompleteEditing?.Invoke(currentAsset);
            
            OnCompleteEditingAfter?.Invoke();
        }
        
        public PaletteAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Palette has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
        
    }
}