using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to look like something and have animations.
    /// </summary>
    public class CharacteristicVisual : CharacteristicBase
    {
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private CharVisualInfo defaultData;

        private void Awake() => renderer.sprite = defaultData.baseSprite;

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharVisualInfo newInfo)
        {
            defaultData = newInfo;
            renderer.sprite = defaultData.baseSprite;
        }
    }
}