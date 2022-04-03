using System.Collections;
using System.Collections.Generic;
using BoubakProductions.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BoubakProductions.UI
{
    public class FlexibleGridGroup : LayoutGroup
    {
        [SerializeField] private AlignmentType alignment;
        [SerializeField] private FitType fitType;

        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 cellSize;
        [SerializeField] private Vector2 spacing;

        [SerializeField] private bool fitX;
        [SerializeField] private bool fitY;

        [SerializeField] private bool onlyActiveChildren;

        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputHorizontal();

            int childCount = (onlyActiveChildren) ? gameObject.GetChildCountActive() : transform.childCount;
            
            if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
            {
                fitX = true;
                fitY = true;
                float sqrRt = Mathf.Sqrt(childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }

            if (fitType == FitType.Width || fitType == FitType.FixedColumns || fitType == FitType.Uniform)
            {
                rows = Mathf.CeilToInt(childCount / (float)columns);
            }
            if (fitType == FitType.Height || fitType == FitType.FixedRows || fitType == FitType.Uniform)
            {
                columns = Mathf.CeilToInt(childCount / (float)rows);
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth;
            float cellHeight;

            if (alignment == AlignmentType.Horizontal)
            {
                cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
                cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);
            }
            else
            {
                cellHeight = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
                cellWidth = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);
            }

            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {

                RectTransform item = rectChildren[i];

                if (alignment == AlignmentType.Horizontal)
                {
                    rowCount = i / columns;
                    columnCount = i % columns;
                    float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                    float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                    SetChildAlongAxis(item, 0, xPos, cellSize.x);
                    SetChildAlongAxis(item, 1, yPos, cellSize.y);
                }
                else
                {
                    rowCount = i / rows;
                    columnCount = i % rows;
                    float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                    float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                    SetChildAlongAxis(item, 0, yPos, cellSize.y);
                    SetChildAlongAxis(item, 1, xPos, cellSize.x);
                }

            }

        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
    }
}