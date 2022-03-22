using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManagerRoom : ToolBoxUIManagerBase
    {
        private ToolBox<AssetData, Sprite> toolbox;

        private void Start()
        {
            toolbox ??= RoomEditorOverseerMono.GetInstance().Toolbox;
            toolbox.OnSwitchTool += SwitchTool;
        }

    }
}