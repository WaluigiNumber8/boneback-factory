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
        public AssetData useSound;
        
        public CharSoundInfo(AssetData hurtSound, AssetData deathSound, AssetData useSound)
        {
            this.hurtSound = hurtSound;
            this.deathSound = deathSound;
            this.useSound = useSound;
        }
    }
}