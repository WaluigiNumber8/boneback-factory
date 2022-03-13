using Rogium.Editors.Rooms;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManagerRoom : ToolBoxUIManagerBase
    {
        private ToolBoxAsset<string> toolbox;

        private void Start()
        {
            toolbox ??= RoomEditorOverseerMono.GetInstance().Toolbox;
            toolbox.OnSwitchTool += SwitchTool;
        }

    }
}