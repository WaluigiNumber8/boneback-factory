using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Contains extension methods for <see cref="Light"/>.
    /// </summary>
    public static class LightExtensions
    {
        public static void CopyInto(this Light original, Light newLight)
        {
            if (newLight == null) return;
            
            // Copy properties
            newLight.type = original.type;
            newLight.color = original.color;
            newLight.intensity = original.intensity;
            newLight.range = original.range;
            newLight.spotAngle = original.spotAngle;
            newLight.cookie = original.cookie;
            newLight.cookieSize = original.cookieSize;
            newLight.shadows = original.shadows;
            newLight.shadowStrength = original.shadowStrength;
            newLight.shadowResolution = original.shadowResolution;
            newLight.shadowBias = original.shadowBias;
            newLight.shadowNormalBias = original.shadowNormalBias;
            newLight.shadowNearPlane = original.shadowNearPlane;
            newLight.renderMode = original.renderMode;
            newLight.cullingMask = original.cullingMask;
            newLight.lightmapBakeType = original.lightmapBakeType;
            newLight.areaSize = original.areaSize;
            newLight.bounceIntensity = original.bounceIntensity;
            newLight.colorTemperature = original.colorTemperature;
            newLight.useColorTemperature = original.useColorTemperature;
            newLight.flare = original.flare;
            newLight.renderingLayerMask = original.renderingLayerMask;
            newLight.lightShadowCasterMode = original.lightShadowCasterMode;
        }
    }
}