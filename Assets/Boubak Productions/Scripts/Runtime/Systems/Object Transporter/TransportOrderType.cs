namespace BoubakProductions.Systems.ObjectTransport
{
    /// <summary>
    /// The different orders, a Transform can be transported.
    /// When axis is missing, it will run parallel to the other ones.
    /// </summary>
    public enum TransportOrderType
    {
        XYZ = 0,
        XZY = 1,
        YXZ = 2,
        YZX = 3,
        ZXY = 4,
        ZYX = 5,
        XY = 6,
        XZ = 7,
        YX = 8,
        YZ = 9,
        ZX = 10,
        ZY = 11,
        X = 12,
        Y = 13,
        Z = 14,
    }
}