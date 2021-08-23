using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridGroup : LayoutGroup
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 cellSize;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrt);
        columns = Mathf.CeilToInt(sqrt);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)rows;
        float cellHeight = parentHeight / (float)columns;

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount, rowCount;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            RectTransform child = rectChildren[i];
            float posX = (cellSize.x * columnCount);
            float posY = (cellSize.y * rowCount);

            SetChildAlongAxis(child, 0, posX, cellSize.x);
            SetChildAlongAxis(child, 1, posY, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
