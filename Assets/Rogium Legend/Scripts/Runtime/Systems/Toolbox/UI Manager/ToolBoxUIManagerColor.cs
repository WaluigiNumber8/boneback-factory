using Rogium.Editors.Sprites;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManagerColor : ToolBoxUIManagerBase
    {
        private ToolBoxColor toolbox;

        private void Start()
        {
            toolbox ??= SpriteEditorOverseerMono.GetInstance().Toolbox;
            toolbox.OnSwitchTool += SwitchTool;
        }
    }
}