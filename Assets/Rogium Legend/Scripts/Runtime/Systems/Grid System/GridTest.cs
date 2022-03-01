using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    public class GridTest : MonoBehaviour
    {
        private InteractableEditorGrid grid;
        private RectTransform gridRect;

        private Vector2 interval;
        private void Start()
        {
            gridRect = GetComponent<RectTransform>();
            grid = GetComponent<InteractableEditorGrid>();

            Vector2Int gridSize = grid.GridSize;
            interval = new Vector2((gridRect.rect.width - gridRect.position.x) / gridSize.x,
                                   (gridRect.rect.height - gridRect.position.y) / gridSize.y);
        }

        private void OnEnable()
        {
            InputOverseer.Instance.UI.PointerPosition.OnPressed += Calculate;
        }

        private void OnDisable()
        {
            InputOverseer.Instance.UI.PointerPosition.OnPressed -= Calculate;
        }


        private void Calculate(Vector2 pointerPos)
        {
            Vector2Int relativeMousePos = new Vector2Int(Mathf.FloorToInt(pointerPos.x * -1 / interval.x),
            Mathf.FloorToInt(pointerPos.y * -1 / interval.y));
            Debug.Log($"Mouse: {pointerPos} | Grid: {relativeMousePos}");

            // gridOverseer.UpdateCellSprite(relativeMousePos, EditorDefaults.TileIcon);
        }
    }
}