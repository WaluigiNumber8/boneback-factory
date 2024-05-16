using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that loads an editor grid with data.
    /// </summary>
    public class LoadEditorGridAction<T> : IAction
    {
        private readonly ObjectGrid<T> grid;
        private readonly ObjectGrid<T> value;
        private readonly ObjectGrid<T> lastValue;
        private readonly Sprite graphicValue;
        private readonly Sprite lastGraphicValue;
        private readonly InteractableEditorGridBase gridCanvas;
        private readonly int layer;

        public LoadEditorGridAction(ObjectGrid<T> grid, ObjectGrid<T> value, ObjectGrid<T> lastValue, Sprite graphicValue, Sprite lastGraphicValue, InteractableEditorGridBase gridCanvas)
        {
            this.grid = grid;
            this.value = value;
            this.lastValue = lastValue;
            this.graphicValue = graphicValue;
            this.lastGraphicValue = lastGraphicValue;
            this.gridCanvas = gridCanvas;
            this.layer = gridCanvas.ActiveLayer;
        }
        
        public void Execute()
        {
            grid.SetFrom(value);
            gridCanvas.LoadWithSprite(graphicValue, layer);
        }

        public void Undo()
        {
            grid.SetFrom(lastValue);
            gridCanvas.LoadWithSprite(lastGraphicValue, layer);
        }

        public bool NothingChanged() => grid.Equals(value);

        public object AffectedConstruct { get => grid; }

        public override string ToString() => $"{layer} - {lastValue} -> {value}";
    }
}