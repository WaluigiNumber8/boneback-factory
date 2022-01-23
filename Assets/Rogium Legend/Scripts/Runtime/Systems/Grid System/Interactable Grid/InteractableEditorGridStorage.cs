using System;
using BoubakProductions.Safety;
using Rogium.Systems.ItemPalette;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Contains editor grids and the respective palettes that can be used on those grids.
    /// </summary>
    public class InteractableEditorGridStorage : MonoBehaviour
    {
        [SerializeField] private GridInfo[] gridData;

        private InteractableEditorGrid currentGrid;
        private ItemPaletteAsset currentPalette;
        
        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {

        }

        public void ChangeGridAndPalette(int index)
        {
            SafetyNet.EnsureIntIsInRange(index, 0, gridData.Length, "Grids index");
            currentGrid = gridData[index].grid;
            currentPalette = gridData[index].itemPalette;
        }
        
        [System.Serializable]
        public struct GridInfo
        {
            public InteractableEditorGrid grid;
            public ItemPaletteAsset itemPalette;
        }
    }
}