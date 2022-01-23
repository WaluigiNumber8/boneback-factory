using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    [RequireComponent(typeof(UIInput))]
    public class GridTest : MonoBehaviour
    {
        private InteractableEditorGrid grid;
        private RectTransform gridRect;
        private UIInput input;

        private Vector2 interval;
        private void Start()
        {
            gridRect = GetComponent<RectTransform>();
            input = GetComponent<UIInput>();
            grid = GetComponent<InteractableEditorGrid>();

            Vector2Int gridSize = grid.GridSize;
            interval = new Vector2((gridRect.rect.width - gridRect.position.x) / gridSize.x,
                                   (gridRect.rect.height - gridRect.position.y) / gridSize.y);
        }

        private void Update()
        {
            Vector2 mousePos = input.PointerPosition;
            Vector2Int relativeMousePos = new Vector2Int(Mathf.FloorToInt(mousePos.x * -1 / interval.x),
                                                         Mathf.FloorToInt(mousePos.y * -1 / interval.y));
            Debug.Log($"Mouse: {mousePos} | Grid: {relativeMousePos}");

            // gridOverseer.UpdateCellSprite(relativeMousePos, EditorDefaults.TileIcon);
        }
    }
}