using System;
using System.Collections.Generic;
using RedRats.Safety;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The Bucket Fill Tool, that fills an area of cells.
    /// </summary>
    public class BucketTool<T> : ITool<T> where T : IComparable
    {
        private ObjectGrid<T> grid;
        private T currentValue;
        private T valueToOverride;
        private readonly IList<Vector2Int> criticalPositions;

        private Action<Vector2Int, bool> applyOnUI;
        private Action finishProcess;

        public BucketTool() => criticalPositions = new List<Vector2Int>();

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI, Action finishProcess)
        {
            SafetyNet.EnsureIsNotNull(grid, "grid");
            
            this.grid = grid;
            this.applyOnUI = applyOnUI;
            this.finishProcess = finishProcess;
            this.currentValue = value;
            this.valueToOverride = GetFrom(position);
            this.criticalPositions.Clear();

            //Return if value on the grid is the same as the bucket fill value.
            if (valueToOverride.CompareTo(currentValue) == 0) return;
            
            //Go to the left-most pixel
            Vector2Int startPos = GetLeftmostPosition(position);

            //Iterative go to the right
            ProcessLoop(startPos);
        }

        /// <summary>
        /// Processes the algorithm for individual position.
        /// </summary>
        /// <param name="pos">The position to process.</param>
        private void ProcessLoop(Vector2Int pos)
        {
            // Check if grid pixel on top/bottom is critical. If it is, add it to a list.
            CheckCellIsCritical(pos + Vector2Int.up);
            CheckCellIsCritical(pos + Vector2Int.down);
            
            //Set value to cell.
            grid.SetValue(pos, currentValue);
            applyOnUI?.Invoke(pos, false);
            
            //Once we reach the end, jump to on of the critical pixels.
            if (pos.x >= grid.Width - 1 || GetFrom(pos + Vector2Int.right).CompareTo(valueToOverride) != 0)
            {
                if (criticalPositions.Count > 0)
                {
                    Vector2Int newPosition = criticalPositions[^1];
                    criticalPositions.RemoveAt(criticalPositions.Count-1);
                    ProcessLoop(newPosition);
                }
                else
                {
                    finishProcess?.Invoke();
                    return;
                }
            }
            //Increment pixel x
            else ProcessLoop(pos + Vector2Int.right);
        }
        
        /// <summary>
        /// Checks if a pixel is considered "critical." If it is, it's added to teh critical pixel list
        /// and later used as a starting position for filling.
        /// </summary>
        /// <param name="pos">The position to check for.</param>
        private void CheckCellIsCritical(Vector2Int pos)
        {
            if (pos.x < 0) return;
            if (pos.y >= grid.Height || pos.y < 0) return;
            
            T value = GetFrom(pos);

            //Is the cell not filled?
            if (value.CompareTo(valueToOverride) != 0) return;
            
            //Grab the edge cell and add it to the list.
            Vector2Int edgeCellPosition = GetLeftmostPosition(pos);
            criticalPositions.Add(edgeCellPosition);
        }
        
        /// <summary>
        /// Finds the leftmost position in the current row.
        /// </summary>
        /// <param name="currentPos">The position to check from.</param>
        /// <returns>The leftmost position found.</returns>
        /// <exception cref="RecursionException">Is thrown when the method fails to return a position or find a new one.</exception>
        private Vector2Int GetLeftmostPosition(Vector2Int currentPos)
        {
            if (currentPos.x <= 0) return currentPos;
            
            T valueToTheLeft = GetFrom(currentPos + Vector2Int.left);
            if (valueToTheLeft.CompareTo(valueToOverride) != 0)
            {
                return currentPos;
            }

            return GetLeftmostPosition(currentPos + Vector2Int.left);
        }

        /// <summary>
        /// Returns a value from the current grid.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private T GetFrom(Vector2Int pos)
        {
            SafetyNet.EnsureIsNotNull(grid, "Value Grid");
            return grid.GetValue(pos);
        }
        
    }
}