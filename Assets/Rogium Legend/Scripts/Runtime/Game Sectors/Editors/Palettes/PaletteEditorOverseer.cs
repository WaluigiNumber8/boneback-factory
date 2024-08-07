using System;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Systems.IconBuilders;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the Palette Editor.
    /// </summary>
    public sealed class PaletteEditorOverseer : Singleton<PaletteEditorOverseer>, IEditorOverseer
    {
        public event Action<PaletteAsset> OnAssignAsset;
        public event Action OnCompleteEditingBefore, OnCompleteEditingAfter;
        public event Action<PaletteAsset, int> OnCompleteEditing;

        private readonly IconBuilder iconBuilder;
        
        private PaletteAsset currentAsset;
        private int myIndex;
        
        private PaletteEditorOverseer() => iconBuilder = new IconBuilder();

        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(PaletteAsset asset, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Palette");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");

            currentAsset = new PaletteAsset.Builder().AsCopy(asset).Build();
            myIndex = index;

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
            currentAsset = new PaletteAsset.Builder().AsCopy(updatedAsset).Build();
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
            OnCompleteEditingBefore?.Invoke();

            Sprite newIcon = iconBuilder.BuildFromArray(currentAsset.Colors);
            newIcon.name = currentAsset.Title;
            currentAsset.UpdateIcon(newIcon);
            OnCompleteEditing?.Invoke(currentAsset, myIndex);
            
            OnCompleteEditingAfter?.Invoke();
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