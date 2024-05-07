namespace RedRats.Systems.ObjectTransport
{
    /// <summary>
    /// The different orders something can be transported.
    /// When axis is missing it will run parallel to the other ones.
    /// </summary>
    public enum TransportOrderType
    {
        XY = 0,
        YX = 1
    }
}