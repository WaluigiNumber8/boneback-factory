using RedRats.Safety;
using System;
using RedRats.Core;
using Rogium.Editors.Palettes;
using Rogium.Systems.IconBuilders;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Overseers the Sprite Editor.
    /// </summary>
    public sealed class SpriteEditorOverseer : Singleton<SpriteEditorOverseer>, IEditorOverseer
    {
        public event Action<SpriteAsset> OnAssignAsset;
        public event Action OnCompleteEditingBefore, OnCompleteEditingAfter;
        public event Action<SpriteAsset, int> OnCompleteEditing;

        private readonly IconBuilder iconBuilder;
        private readonly PalettePicker palettePicker;
        
        private SpriteAsset currentAsset;
        private PaletteAsset currentPalette;
        private int myIndex;
        
        private SpriteEditorOverseer()
        {
            iconBuilder = new IconBuilder();
            palettePicker = new PalettePicker();
        }

        /// <summary>
        /// Assign an asset, that is going to be edited.
        /// </summary>
        /// <param name="asset">The asset that is going to be edited.</param>
        /// <param name="index">Asset's list index. (For updating)</param>
        /// <param name="prepareEditor"></param>
        public void AssignAsset(SpriteAsset asset, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Sprite");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned asset index");
            
            currentAsset = new SpriteAsset(asset);
            currentPalette = palettePicker.GrabBasedOn(currentAsset.PreferredPaletteID);
            myIndex = index;
            
            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentAsset);
        }
        
        /// <summary>
        /// Updates the sprite asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset containing new data.</param>
        public void UpdateAsset(SpriteAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new SpriteAsset(updatedAsset);
        }

        public void UpdatePalette(PaletteAsset updatedPalette)
        {
            SafetyNet.EnsureIsNotNull(currentPalette, "Currently active palette.");
            currentPalette = new PaletteAsset.Builder().AsCopy(updatedPalette).Build();
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditingBefore?.Invoke();

            Sprite newIcon = iconBuilder.BuildFromGrid(currentAsset.SpriteData, currentPalette.Colors);
            newIcon.name = currentAsset.Title;
            currentAsset.UpdateIcon(newIcon);
            
            OnCompleteEditing?.Invoke(currentAsset, myIndex);
            OnCompleteEditingAfter?.Invoke();
        }

        public SpriteAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Sprite has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
        
        public PaletteAsset CurrentPalette { get => currentPalette; }
        
    }
}