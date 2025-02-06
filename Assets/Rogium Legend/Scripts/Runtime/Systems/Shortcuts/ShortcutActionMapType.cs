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
        GeneralSelection = 1 << 1,
        SelectionMenu = 1 << 2,
        CampaignSelection = 1 << 3,
        DrawingEditors = 1 << 4,
        SpriteEditor = 1 << 5,
        RoomEditor = 1 << 6,
        CampaignEditor = 1 << 7,
    }
}