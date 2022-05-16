using RedRats.Core;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all animated assets.
    /// </summary>
    public class AnimatedAssetBase : AssetBase
    {
        protected AnimationType animationType;
        protected int frameDuration;
        protected Sprite iconAlt;

        #region Update Values
        public void UpdateAnimationType(AnimationType newType) => animationType = newType;
        public void UpdateFrameDuration(int newFrameDuration)
        {
            newFrameDuration = Mathf.Clamp(newFrameDuration, 0, AssetValidation.MaxFrameDuration);
            frameDuration = newFrameDuration;
        }

        public void UpdateIconAlt(Sprite newIconAlt) => iconAlt = newIconAlt;
        #endregion
        
        public AnimationType AnimationType { get => animationType; }
        public int FrameDuration { get => frameDuration; }
        public Sprite IconAlt { get => iconAlt; }
    }
}