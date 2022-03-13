using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicVisual"/> characteristic.
    /// </summary>
    [System.Serializable]
    public struct CharVisualInfo
    {
        public Sprite baseSprite;

        public CharVisualInfo(Sprite baseSprite)
        {
            this.baseSprite = baseSprite;
        }
    }
}