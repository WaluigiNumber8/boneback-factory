using RedRats.UI.Core.Cursors;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Systems.Toolbox;

namespace Rogium.UserInterface.Cursors
{
    /// <summary>
    /// Changes the cursor to the current active tool when hovering over a UI element.
    /// </summary>
    public class CursorChangerToolbox : CursorChangerBase
    {
        private CursorType currentCursor;

        protected override void OnEnable()
        {
            base.OnEnable();
            RoomEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool += UpdateCursor;
            SpriteEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool += UpdateCursor;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RoomEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool -= UpdateCursor;
            SpriteEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool -= UpdateCursor;
        }

        private void UpdateCursor(ToolType tool)
        {
            currentCursor = tool switch
            {
                ToolType.Selection => CursorType.Default,
                ToolType.Brush => CursorType.ToolBrush,
                ToolType.Eraser => CursorType.ToolEraser,
                ToolType.Fill => CursorType.ToolFill,
                ToolType.ColorPicker => CursorType.ToolPicker,
                _ => CursorType.Default
            };
        }

        protected override CursorType CursorToSet { get => currentCursor; }
    }
}