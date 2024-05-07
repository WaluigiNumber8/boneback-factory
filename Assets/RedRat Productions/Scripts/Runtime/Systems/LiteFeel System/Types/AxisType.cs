using System;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// The different combinations of axis.
    /// </summary>
    [Flags]
    public enum AxisType
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2
    }
}