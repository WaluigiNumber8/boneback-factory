using Rogium.Editors.Core;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicSoundEmitter"/> characteristic.
    /// </summary>
    public struct CharSoundInfo
    {
        public AssetData hurtSound;
        public AssetData deathSound;
        public AssetData idleSound;
        public AssetData useSound;
        
        public CharSoundInfo(AssetData hurtSound, AssetData deathSound, AssetData idleSound, AssetData useSound)
        {
            this.hurtSound = hurtSound;
            this.deathSound = deathSound;
            this.idleSound = idleSound;
            this.useSound = useSound;
        }
    }
}