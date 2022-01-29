using System;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// A base for all toolboxes.
    /// </summary>
    public interface IToolBox
    {
        public event Action<ToolType> OnSwitchTool; 

        /// <summary>
        /// Switches to a new tool.
        /// </summary>
        /// <param name="tool">Tool Type of the new tool.</param>
        /// <exception cref="InvalidOperationException">Is thrown when the Tool is not supported.</exception>
        void SwitchTool(ToolType tool);

        /// <summary>
        /// Draws on the UI grid.
        /// </summary>
        /// <param name="position">The position to draw on.</param>
        /// <param name="useEmpty">If true, use the empty value instead.</param>
        void WhenDrawOnUIGrid(Vector2Int position, bool useEmpty);
    }
}