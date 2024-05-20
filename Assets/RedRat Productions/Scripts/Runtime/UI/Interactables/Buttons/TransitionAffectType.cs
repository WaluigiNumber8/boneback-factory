using System;

namespace RedRats.UI.Core.Interactables.Buttons
{
    /// <summary>
    /// The different things that can be affected by a transition.
    /// </summary>
    [Flags]
    public enum TransitionAffectType
    {
        Color = 1 << 0,
        Sprite = 1 << 1,
    }
}