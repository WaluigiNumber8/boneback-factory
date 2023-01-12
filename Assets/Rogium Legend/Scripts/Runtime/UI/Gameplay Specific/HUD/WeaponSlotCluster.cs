using Rogium.Editors.Core.Defaults;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Gameplay.HUD
{
    /// <summary>
    /// Controls a slot holding a weapon.
    /// </summary>
    public class WeaponSlotCluster : MonoBehaviour
    {
        [SerializeField] private Image mainIcon;
        [SerializeField] private Image subIcon;

        /// <summary>
        /// Assigns a new icon for the all weapon slots.
        /// </summary>
        /// <param name="mainSprite">The sprite of the main icon.</param>
        /// <param name="subSprite">The sprite of the sub icon.</param>
        public void Set(Sprite mainSprite, Sprite subSprite)
        {
            SetMain(mainSprite);
            SetSub(subSprite);
        }

        /// <summary>
        /// Assigns a new icon for the Main weapon slot.
        /// </summary>
        /// <param name="sprite">The sprite of the icon.</param>
        public void SetMain(Sprite sprite)
        {
            mainIcon.color = EditorConstants.DefaultColor;
            mainIcon.sprite = sprite;
        }

        /// <summary>
        /// Assigns a new icon for the Sub weapon slot.
        /// </summary>
        /// <param name="sprite">The sprite of the icon.</param>
        public void SetSub(Sprite sprite)
        {
            subIcon.color = EditorConstants.DefaultColor;
            subIcon.sprite = sprite;
        }

        /// <summary>
        /// Empties all slots.
        /// </summary>
        public void Empty()
        {
            EmptyMain();
            EmptySub();
        }
        /// <summary>
        /// Empties the Main Weapon Slot.
        /// </summary>
        public void EmptyMain()
        {
            mainIcon.color = EditorConstants.NoColor;
            mainIcon.sprite = null;
        }

        /// <summary>
        /// Empties the Sub Weapon Slot.
        /// </summary>
        public void EmptySub()
        {
            subIcon.color = EditorConstants.NoColor;
            subIcon.sprite = null;
        }
    }
}