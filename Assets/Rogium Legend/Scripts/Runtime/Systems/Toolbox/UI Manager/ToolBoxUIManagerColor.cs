using Rogium.Editors.Sprites;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManagerColor : ToolBoxUIManagerBase
    {
        private ToolBox<int, Color> toolbox;

        private void Start()
        {
            toolbox ??= SpriteEditorOverseerMono.GetInstance().Toolbox;
            toolbox.OnSwitchTool += SwitchTool;
        }
    }
}