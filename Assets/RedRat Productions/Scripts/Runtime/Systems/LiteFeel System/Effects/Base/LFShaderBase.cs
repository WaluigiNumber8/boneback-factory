using RedRats.Core.Utils;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFShaderBase : LFEffectTweenMultipleBase
    {
        [Header("Target")] 
        [SerializeField] private MaterialExtractor target;

        protected Material material;

        protected override void Initialize()
        {
            material = target.Get();
        }
    }
}