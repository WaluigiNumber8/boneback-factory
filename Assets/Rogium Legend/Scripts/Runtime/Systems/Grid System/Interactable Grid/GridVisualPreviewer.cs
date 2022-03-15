using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.ItemPalette;
using Rogium.UserInterface.AssetSelection;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Controls the Visual Preview on the Interactable grid, so that the user knows, what he is using.
    /// </summary>
    public class GridVisualPreviewer : MonoBehaviour
    {
        [SerializeField] private InteractableEditorGridV2 grid;
        [SerializeField] private PreviewerInfo gridPreviewer;
        [Header("Palettes")]
        [SerializeField] private ItemPaletteAsset[] assetPalettes;
        [SerializeField] private ItemPaletteColor[] colorPalettes;
        
        private RectTransform gridTransform;

        private void OnEnable()
        {
            grid.OnPointerComeIn += SwitchVisibilityStatus;
            grid.OnPointerLeave += SwitchVisibilityStatus;
            
            foreach (ItemPaletteAsset palette in assetPalettes)
            {
                if (palette != null) palette.OnSelect += ChangeMaterial;
            }
            foreach (ItemPaletteColor palette in colorPalettes)
            {
                if (palette != null) palette.OnSelect += ChangeMaterial;
            }
        }

        private void OnDisable()
        {
            grid.OnPointerComeIn -= SwitchVisibilityStatus;
            grid.OnPointerLeave -= SwitchVisibilityStatus;
            
            foreach (ItemPaletteAsset palette in assetPalettes)
            {
                if (palette != null) palette.OnSelect -= ChangeMaterial;
            }
            foreach (ItemPaletteColor palette in colorPalettes)
            {
                if (palette != null) palette.OnSelect -= ChangeMaterial;
            }
        }

        private void Start()
        {
            gridPreviewer.gameObject.SetActive(false);
            gridTransform = grid.GetComponent<RectTransform>();
            gridPreviewer.transform.position = gridTransform.anchoredPosition;
            gridPreviewer.transform.sizeDelta = grid.CellSize;
        }

        private void Update()
        {
            GridPreviewerFollow();   
        }

        /// <summary>
        /// Handles Grid Previewers following on the grid.
        /// </summary>
        private void GridPreviewerFollow()
        {
            if (!gridPreviewer.gameObject.activeSelf) return;
            
            float x = gridTransform.position.x + grid.CellSize.x * grid.SelectedPosition.x + grid.SelectedPosition.x;
            float y = gridTransform.position.y + grid.CellSize.y * grid.SelectedPosition.y + grid.SelectedPosition.y;
            gridPreviewer.transform.position = new Vector3(x, y, 0);
        }

        /// <summary>
        /// Switch visibility of the previewer.
        /// </summary>
        private void SwitchVisibilityStatus()
        {
            gridPreviewer.gameObject.SetActive(!gridPreviewer.gameObject.activeSelf);
        }

        private void ChangeMaterial(AssetSlot slot)
        {
            gridPreviewer.image.color = EditorDefaults.DefaultColor;
            gridPreviewer.image.sprite = slot.Asset.Icon;
        }
        
        private void ChangeMaterial(ColorSlot slot)
        {
            gridPreviewer.image.sprite = null;
            gridPreviewer.image.color = slot.CurrentColor;
        }
        
        [System.Serializable]
        public struct PreviewerInfo
        {
            public GameObject gameObject;
            public RectTransform transform;
            public Image image;
        }
    }
}