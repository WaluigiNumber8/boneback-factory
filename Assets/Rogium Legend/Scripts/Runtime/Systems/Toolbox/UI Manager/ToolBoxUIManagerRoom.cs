using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManagerRoom : ToolBoxUIManagerBase
    {
        private ToolBox<string, Sprite> toolbox;

        private void Start()
        {
            toolbox ??= RoomEditorOverseerMono.GetInstance().Toolbox;
            toolbox.OnSwitchTool += SwitchTool;
        }

    }
}