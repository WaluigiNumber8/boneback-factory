using RedRats.Systems.LiteFeel.Core;
using Rogium.Gameplay.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a weapon.
    /// </summary>
    public class LFBrainWeapon : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private WeaponController weapon;
        [Space] 
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.05f, 1f, 0.25f)] private LFEffector onUseEffect;

        private void OnEnable()
        {
            if (onUseEffect != null) weapon.OnUse += onUseEffect.Play;
        }

        private void OnDisable()
        {
            if (onUseEffect != null) weapon.OnUse -= onUseEffect.Play;
        }

    }
}