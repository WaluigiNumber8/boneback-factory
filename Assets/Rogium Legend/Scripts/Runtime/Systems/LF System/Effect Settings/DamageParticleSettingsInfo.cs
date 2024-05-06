using System;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel
{
    /// <summary>
    /// Settings for a damage particle effect, that scales with damage.
    /// </summary>
    [Serializable]
    public struct DamageParticleSettingsInfo
    {
        [Required] public LFParticleEffect particleEffect;
        public AnimationCurve amountCurve;
        [MinValue(0)] public int minDamage;
        [MinValue(0)] public int maxDamage;
        [MinValue(0)] public int minAmount;
        [MinValue(0)] public int maxAmount;
        public bool isDirectional;

        public int GetAmountOfParticles(int damage)
        {
            return RedRatUtils.RemapAndEvaluate(damage, amountCurve, minDamage, maxDamage, minAmount, maxAmount);
        }
    }
}