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
        private readonly ObjectGrid<T> oldValue;
        private readonly Sprite graphicValue;
        private readonly Sprite oldGraphicValue;
        private readonly InteractableEditorGridBase gridCanvas;
        private readonly int layer;

        public LoadEditorGridAction(ObjectGrid<T> grid, ObjectGrid<T> value, ObjectGrid<T> oldValue, Sprite graphicValue, Sprite oldGraphicValue, InteractableEditorGridBase gridCanvas)
        {
            this.grid = grid;
            this.value = value;
            this.oldValue = oldValue;
            this.graphicValue = graphicValue;
            this.oldGraphicValue = oldGraphicValue;
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
            grid.SetFrom(oldValue);
            gridCanvas.LoadWithSprite(oldGraphicValue, layer);
        }

        public bool NothingChanged() => grid.Equals(value);

        public object AffectedConstruct { get => grid; }

        public override string ToString() => $"{layer} - {oldValue} -> {value}";
    }
}