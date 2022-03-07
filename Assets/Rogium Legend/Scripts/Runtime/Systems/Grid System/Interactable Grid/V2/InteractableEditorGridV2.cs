using System;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// An upgraded version of <see cref="InteractableEditorGrid"/>.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class InteractableEditorGridV2 : InteractableEditorGridBase, IPointerClickHandler, IPointerMoveHandler
    {
        public override event Action<Vector2Int> OnClick;
        
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Image[] layers;
        
        private RectTransform ttransform;
        private SpriteDrawer drawer;
        
        private Image activeLayer;
        
        private Vector2Int cellSize;
        private Vector2Int selectedPos;
        
        private void Awake()
        {
            SafetyNet.EnsureIntIsBiggerThan(layers.Length, 0, "Layers amount");
            
            ttransform = GetComponent<RectTransform>();
            cellSize = new Vector2Int((int)ttransform.rect.width / gridSize.x, (int)ttransform.rect.height / gridSize.y);
            
            drawer = new SpriteDrawer(gridSize, new Vector2Int(EditorDefaults.SpriteSize, EditorDefaults.SpriteSize), EditorDefaults.SpriteSize);
            PrepareLayers();
            SwitchActiveLayer(0);
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           RecalculateSelectedPosition(eventData.position);
           OnClick?.Invoke(selectedPos);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!InputSystem.Instance.UI.Click.IsHeld) return;
            RecalculateSelectedPosition(eventData.position);
            OnClick?.Invoke(selectedPos);
        }

        public override void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid)
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            activeLayer.sprite = drawer.Build(IDGrid, assetList);
        }
        
        public override void LoadWithColors(Color[] colorArray, ObjectGrid<int> indexGrid)
        {
            SafetyNet.EnsureIntIsEqual(indexGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(indexGrid.Height, gridSize.y, "Grid Height");
            activeLayer.sprite = drawer.Build(indexGrid, colorArray);
        }
        
        public override void UpdateCell(Vector2Int position, Color value)
        {
            drawer.UpdateValue(activeLayer.sprite, position, value);
        }
        
        public override void UpdateCell(Vector2Int position, Sprite value)
        {
            drawer.UpdateValue(activeLayer.sprite, position, value);
        }

        public override void Apply() => drawer.Apply(activeLayer.sprite);
        
        /// <summary>
        /// Switches the currently active layer.
        /// </summary>
        /// <param name="layerIndex">The index of the active layer.</param>
        public void SwitchActiveLayer(int layerIndex)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(layerIndex, layers, nameof(layers));
            activeLayer = layers[layerIndex];
        }
        
        /// <summary>
        /// Updates the currently selected grid position based on the pointer.
        /// </summary>
        private void RecalculateSelectedPosition(Vector2 pointerPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ttransform, pointerPos, null, out Vector2 pos);
            
            int x = (int)Mathf.Abs(Mathf.Floor(pos.x / cellSize.x));
            int y = (int)Mathf.Abs(Mathf.Floor(pos.y / cellSize.y));

            x = Mathf.Max(0, Mathf.Min(x, gridSize.x-1));
            y = Mathf.Max(0, Mathf.Min(y, gridSize.y-1));
            
            selectedPos = new Vector2Int(x, y);
        }

        /// <summary>
        /// Prepares the grid layers.
        /// </summary>
        private void PrepareLayers()
        {
            foreach (Image layer in layers)
            {
                layer.color = Color.white;
                layer.sprite = BoubakBuilder.GenerateSprite(EditorDefaults.NoColor,
                                                            EditorDefaults.SpriteSize * gridSize.x,
                                                            EditorDefaults.SpriteSize * gridSize.y,
                                                            EditorDefaults.SpriteSize);
            }
        }
        
    }
}