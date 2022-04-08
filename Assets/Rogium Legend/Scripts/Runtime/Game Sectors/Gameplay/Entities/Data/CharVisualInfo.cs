using Rogium.Editors.Core;
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
        public AnimationType animationType;
        public int frameDuration;
        public Sprite spriteAlt;

        public CharVisualInfo(Sprite baseSprite, AnimationType animationType, int frameDuration, Sprite spriteAlt)
        {
            this.baseSprite = baseSprite;
            this.animationType = animationType;
            this.frameDuration = frameDuration;
            this.spriteAlt = spriteAlt;
        }
    }
}