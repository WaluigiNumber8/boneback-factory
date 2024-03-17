using System;

namespace Rogium.UserInterface.Editors.Utils
{
    /// <summary>
    /// The different ways a UI element can follow the pointer.
    /// </summary>
    [Flags]
    public enum UIFollowType
    {
        OnEnable = 1 << 0,
        OnUpdate = 1 << 1,
        OnDrag = 1 << 2
    }
}