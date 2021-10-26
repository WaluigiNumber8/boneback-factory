namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Has control over which Tool is currently active.
    /// </summary>
    public class ToolBox
    {
        private ToolType currentTool = ToolType.Brush;

        public ToolType CurrentTool { get => currentTool;}
    }
}