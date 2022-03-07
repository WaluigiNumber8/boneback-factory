namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicDamageGiver"/>
    /// </summary>
    [System.Serializable]
    public struct CharDamageGiverInfo
    {
        public int baseDamage;
        public ForcedMoveInfo knockbackSelf;
        public ForcedMoveInfo knockbackOther;

        public CharDamageGiverInfo(int baseDamage, ForcedMoveInfo knockbackSelf, ForcedMoveInfo knockbackOther)
        {
            this.baseDamage = baseDamage;
            this.knockbackSelf = knockbackSelf;
            this.knockbackOther = knockbackOther;
        }
    }
}