using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Controls the Visual Preview on the Interactable grid, so that the user knows, what he is using.
    /// </summary>
    public class GridVisualPreviewer : MonoBehaviour
    {
        [SerializeField] private InteractableEditorGrid grid;
        [SerializeField] private ToolBoxUIManagerBase toolbox;
        [SerializeField] private PreviewerInfo gridPreviewer;
        
        [Header("Palettes")]
        [SerializeField] private ItemPaletteAsset[] assetPalettes;
        [SerializeField] private ItemPaletteColor[] colorPalettes;
        
        [Header("Tool Presets")]
        [SerializeField] private GridPreviewerToolInfoAsset toolInfoAsset;
        
        private RectTransform gridTransform;
        private Vector2 gridScaleOffset;
        
        private ToolType currentTool = ToolType.Eraser;
        private bool inPermanentState;
        private bool allowMaterialSwitching;
        private bool followCursor;

        private Sprite lastMaterial;
        private Color lastColor;

        private void Start()
        {
            gridTransform = grid.GetComponent<RectTransform>();
            gridPreviewer.transform.position = gridTransform.anchoredPosition;
            gridPreviewer.transform.sizeDelta = grid.CellSize;
            gridScaleOffset = new Vector2(gridTransform.rect.width / gridTransform.rect.height,
                                          gridTransform.rect.height / gridTransform.rect.width);
            gridPreviewer.transform.localScale = new Vector3(1f / (grid.Size.x+1), 1f / (grid.Size.y+1));
            
            followCursor = true;
            lastColor = EditorConstants.DefaultColor;
            
            PrepareForTool(ToolType.Brush);
            Hide();
            EnablePermanent();
        }
        
        private void OnEnable()
        {
            grid.OnPointerComeIn += Show;
            grid.OnPointerLeave += Hide;
            grid.OnPointerClicked += UpdatePositionOnGrid;
            toolbox.OnSwitchTool += PrepareForTool;
            
            foreach (ItemPaletteAsset palette in assetPalettes)
            {
                palette.OnSelect += ChangeMaterial;
            }
            foreach (ItemPaletteColor palette in colorPalettes)
            {
                palette.OnSelect += ChangeColor;
            }
        }

        private void OnDisable()
        {
            grid.OnPointerComeIn -= Show;
            grid.OnPointerLeave -= Hide;
            grid.OnPointerClicked -= UpdatePositionOnGrid;
            toolbox.OnSwitchTool -= PrepareForTool;
            
            foreach (ItemPaletteAsset palette in assetPalettes)
            {
                palette.OnSelect -= ChangeMaterial;
            }
            foreach (ItemPaletteColor palette in colorPalettes)
            {
                palette.OnSelect -= ChangeColor;
            }
        }

        private void Update()
        {
            if (!followCursor) return;
            UpdatePositionOnGrid();
        }

        /// <summary>
        /// Handles Grid Previewers following on the grid.
        /// </summary>
        private void UpdatePositionOnGrid()
        {
            if (!gridPreviewer.gameObject.activeSelf) return;
            
            float x = gridTransform.position.x + gridScaleOffset.x + grid.SelectedPosition.x * grid.CellSize.x + grid.CellSize.x * 0.5f;
            float y = gridTransform.position.y + gridScaleOffset.y + grid.SelectedPosition.y * grid.CellSize.y + grid.CellSize.y * 0.5f;
            gridPreviewer.transform.position = new Vector3(x, y);
        }

        #region Visibility Control

        /// <summary>
        /// Show the previewer.
        /// </summary>
        private void Show()
        {
            if (!inPermanentState) return;
            gridPreviewer.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide the previewer.
        /// </summary>
        private void Hide()
        {
            if (inPermanentState) return;
            gridPreviewer.gameObject.SetActive(false);
        }

        /// <summary>
        /// Show the previewer permanently.
        /// </summary>
        private void EnablePermanent() => inPermanentState = true;

        /// <summary>
        /// Hide the previewer permanently.
        /// </summary>
        private void DisablePermanent() => inPermanentState = false;
        #endregion

        #region Material Change

        private void ChangeMaterial(Sprite brush)
        {
            if (assetPalettes.Length <= 0) return;
            lastMaterial = brush;
            
            if (!allowMaterialSwitching) return;
            
            gridPreviewer.image.color = EditorConstants.DefaultColor;
            gridPreviewer.image.sprite = lastMaterial;
        }
        private void ChangeMaterial(IAsset brush) => ChangeMaterial(brush.Icon);

        private void ChangeColor(Color color)
        {
            if (colorPalettes.Length <= 0) return;
            lastColor = color;
            
            if (!allowMaterialSwitching) return;
            
            gridPreviewer.image.sprite = null;
            gridPreviewer.image.color = lastColor;
        }
        private void ChangeColor(ColorSlot slot) => ChangeColor(slot.CurrentColor);

        #endregion

        /// <summary>
        /// Prepares the previewer for a specific tool.
        /// </summary>
        /// <param name="type">The type of tool.</param>
        private void PrepareForTool(ToolType type)
        {
            if (type == currentTool) return;
            foreach (PreviewerToolDataInfo info in toolInfoAsset.ToolInfo)
            {
                if (type != info.tool) continue;
    
                if (info.isVisible) Show(); else Hide();
                if (info.permanentState) EnablePermanent(); else DisablePermanent();
                followCursor = info.followCursor;
                allowMaterialSwitching = info.autoMaterial;
                if (!allowMaterialSwitching)
                {
                    gridPreviewer.image.sprite = info.customSprite;
                    gridPreviewer.image.color = info.customColor;
                }
                else
                {
                    ChangeMaterial(lastMaterial);
                }

                currentTool = type;
            }
        }
        
        [Serializable]
        public struct PreviewerInfo
        {
            public GameObject gameObject;
            public RectTransform transform;
            public Image image;
        }
    }
}