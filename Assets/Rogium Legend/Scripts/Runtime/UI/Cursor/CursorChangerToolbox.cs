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
        private RoomEditorOverseerMono roomEditor;
        private SpriteEditorOverseerMono spriteEditor;
        
        private CursorType currentCursor;

        protected override void Awake()
        {
            base.Awake();
            roomEditor = RoomEditorOverseerMono.GetInstance();
            spriteEditor = SpriteEditorOverseerMono.GetInstance();
        }

        private void OnEnable()
        {
            roomEditor.Toolbox.OnSwitchTool += UpdateCursor;
            spriteEditor.Toolbox.OnSwitchTool += UpdateCursor;
        }

        protected override void OnDisable()
        {
            roomEditor.Toolbox.OnSwitchTool -= UpdateCursor;
            spriteEditor.Toolbox.OnSwitchTool -= UpdateCursor;
            base.OnDisable();
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
            SetIfWithinBounds();
        }

        protected override CursorType CursorToSet { get => currentCursor; }
    }
}