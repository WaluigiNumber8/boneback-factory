using System;

namespace Rogium.Systems.Shortcuts
{
    /// <summary>
    /// All different types of shortcut action maps.
    /// </summary>
    [Flags]
    public enum ShortcutActionMapType
    {
        General = 1 << 0,
        SelectionMenu = 1 << 1,
        CampaignSelection = 1 << 2,
        DrawingEditors = 1 << 3,
        SpriteEditor = 1 << 4,
        RoomEditor = 1 << 5,
        CampaignEditor = 1 << 6,
    }
}