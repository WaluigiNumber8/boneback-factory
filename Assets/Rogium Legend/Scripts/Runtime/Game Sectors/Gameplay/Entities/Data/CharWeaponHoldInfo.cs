namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data for the <see cref="CharacteristicWeaponHold"/> characteristic.
    /// </summary>
    [System.Serializable]
    public struct CharWeaponHoldInfo
    {
        public float useDelay;
        
        public CharWeaponHoldInfo(float useDelay)
        {
            this.useDelay = useDelay;
        }
    }
}