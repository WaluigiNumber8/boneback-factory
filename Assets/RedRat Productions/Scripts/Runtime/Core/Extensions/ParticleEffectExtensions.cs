using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Contains extension methods for the <see cref="ParticleSystem"/> type.
    /// </summary>
    public static class ParticleEffectExtensions
    {
        /// <summary>
        /// Copies all properties from one <see cref="ParticleSystem"/> to another.
        /// </summary>
        /// <param name="original">The original to copy from.</param>
        /// <param name="newEffect">The new one to copy to.</param>
        /// <returns>newEffect with original's properties.</returns>
        public static void CopyInto(this ParticleSystem original, ParticleSystem newEffect)
        {
            if (newEffect == null) return;
            
            if (newEffect.isPlaying) newEffect.Stop();
            
            // Copy main module properties
            ParticleSystem.MainModule originalMain = original.main;
            ParticleSystem.MainModule copyMain = newEffect.main;
            copyMain.duration = originalMain.duration;
            copyMain.loop = originalMain.loop;
            copyMain.startDelay = originalMain.startDelay;
            copyMain.startLifetime = originalMain.startLifetime;
            copyMain.startSpeed = originalMain.startSpeed;
            copyMain.startSize = originalMain.startSize;
            copyMain.startRotation = originalMain.startRotation;
            copyMain.startColor = originalMain.startColor;
            copyMain.gravityModifier = originalMain.gravityModifier;
            copyMain.simulationSpace = originalMain.simulationSpace;
            copyMain.playOnAwake = originalMain.playOnAwake;
            copyMain.maxParticles = originalMain.maxParticles;
            copyMain.stopAction = originalMain.stopAction;
            copyMain.cullingMode = originalMain.cullingMode;
            copyMain.ringBufferMode = originalMain.ringBufferMode;
            copyMain.scalingMode = originalMain.scalingMode;

            // Copy emission module properties
            ParticleSystem.EmissionModule originalEmission = original.emission;
            ParticleSystem.EmissionModule copyEmission = newEffect.emission;
            copyEmission.enabled = originalEmission.enabled;
            copyEmission.rateOverTime = originalEmission.rateOverTime;
            copyEmission.rateOverDistance = originalEmission.rateOverDistance;
            copyEmission.rateOverTimeMultiplier = originalEmission.rateOverTimeMultiplier;
            copyEmission.rateOverDistanceMultiplier = originalEmission.rateOverDistanceMultiplier;
            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[originalEmission.burstCount];
            originalEmission.GetBursts(bursts);
            copyEmission.SetBursts(bursts);

            // Copy shape module properties
            ParticleSystem.ShapeModule originalShape = original.shape;
            ParticleSystem.ShapeModule copyShape = newEffect.shape;
            copyShape.enabled = originalShape.enabled;
            copyShape.shapeType = originalShape.shapeType;
            copyShape.angle = originalShape.angle;
            copyShape.radius = originalShape.radius;
            copyShape.scale = originalShape.scale;
            copyShape.alignToDirection = originalShape.alignToDirection;
            copyShape.randomDirectionAmount = originalShape.randomDirectionAmount;
            copyShape.sphericalDirectionAmount = originalShape.sphericalDirectionAmount;
            copyShape.arc = originalShape.arc;
            copyShape.arcMode = originalShape.arcMode;
            copyShape.arcSpread = originalShape.arcSpread;
            copyShape.arcSpeed = originalShape.arcSpeed;
            copyShape.length = originalShape.length;
            copyShape.position = originalShape.position;
            copyShape.rotation = originalShape.rotation;
            copyShape.scale = originalShape.scale;

            // Copy velocity over lifetime module properties
            ParticleSystem.VelocityOverLifetimeModule originalVelocityOverLifetime = original.velocityOverLifetime;
            ParticleSystem.VelocityOverLifetimeModule copyVelocityOverLifetime = newEffect.velocityOverLifetime;
            copyVelocityOverLifetime.enabled = originalVelocityOverLifetime.enabled;
            copyVelocityOverLifetime.space = originalVelocityOverLifetime.space;
            copyVelocityOverLifetime.x = originalVelocityOverLifetime.x;
            copyVelocityOverLifetime.y = originalVelocityOverLifetime.y;
            copyVelocityOverLifetime.z = originalVelocityOverLifetime.z;
            copyVelocityOverLifetime.orbitalX = originalVelocityOverLifetime.orbitalX;
            copyVelocityOverLifetime.orbitalY = originalVelocityOverLifetime.orbitalY;
            copyVelocityOverLifetime.orbitalZ = originalVelocityOverLifetime.orbitalZ;
            copyVelocityOverLifetime.radial = originalVelocityOverLifetime.radial;
            copyVelocityOverLifetime.speedModifier = originalVelocityOverLifetime.speedModifier;
            copyVelocityOverLifetime.space = originalVelocityOverLifetime.space;
            copyVelocityOverLifetime.orbitalOffsetX = originalVelocityOverLifetime.orbitalOffsetX;
            copyVelocityOverLifetime.orbitalOffsetY = originalVelocityOverLifetime.orbitalOffsetY;
            copyVelocityOverLifetime.orbitalOffsetZ = originalVelocityOverLifetime.orbitalOffsetZ;
            copyVelocityOverLifetime.xMultiplier = originalVelocityOverLifetime.xMultiplier;
            copyVelocityOverLifetime.yMultiplier = originalVelocityOverLifetime.yMultiplier;
            copyVelocityOverLifetime.zMultiplier = originalVelocityOverLifetime.zMultiplier;
            copyVelocityOverLifetime.orbitalXMultiplier = originalVelocityOverLifetime.orbitalXMultiplier;
            copyVelocityOverLifetime.orbitalYMultiplier = originalVelocityOverLifetime.orbitalYMultiplier;
            copyVelocityOverLifetime.orbitalZMultiplier = originalVelocityOverLifetime.orbitalZMultiplier;
            copyVelocityOverLifetime.radialMultiplier = originalVelocityOverLifetime.radialMultiplier;
            copyVelocityOverLifetime.speedModifierMultiplier = originalVelocityOverLifetime.speedModifierMultiplier;

            // Copy limit velocity over lifetime module properties
            ParticleSystem.LimitVelocityOverLifetimeModule originalLimitVelocityOverLifetime = original.limitVelocityOverLifetime;
            ParticleSystem.LimitVelocityOverLifetimeModule copyLimitVelocityOverLifetime = newEffect.limitVelocityOverLifetime;
            copyLimitVelocityOverLifetime.enabled = originalLimitVelocityOverLifetime.enabled;
            copyLimitVelocityOverLifetime.space = originalLimitVelocityOverLifetime.space;
            copyLimitVelocityOverLifetime.dampen = originalLimitVelocityOverLifetime.dampen;
            copyLimitVelocityOverLifetime.drag = originalLimitVelocityOverLifetime.drag;
            copyLimitVelocityOverLifetime.limit = originalLimitVelocityOverLifetime.limit;
            copyLimitVelocityOverLifetime.limitX = originalLimitVelocityOverLifetime.limitX;
            copyLimitVelocityOverLifetime.limitY = originalLimitVelocityOverLifetime.limitY;
            copyLimitVelocityOverLifetime.limitZ = originalLimitVelocityOverLifetime.limitZ;
            copyLimitVelocityOverLifetime.separateAxes = originalLimitVelocityOverLifetime.separateAxes;
            copyLimitVelocityOverLifetime.limitMultiplier = originalLimitVelocityOverLifetime.limitMultiplier;
            copyLimitVelocityOverLifetime.limitXMultiplier = originalLimitVelocityOverLifetime.limitXMultiplier;
            copyLimitVelocityOverLifetime.limitYMultiplier = originalLimitVelocityOverLifetime.limitYMultiplier;
            copyLimitVelocityOverLifetime.limitZMultiplier = originalLimitVelocityOverLifetime.limitZMultiplier;
            copyLimitVelocityOverLifetime.dragMultiplier = originalLimitVelocityOverLifetime.dragMultiplier;
            copyLimitVelocityOverLifetime.multiplyDragByParticleSize = originalLimitVelocityOverLifetime.multiplyDragByParticleSize;
            copyLimitVelocityOverLifetime.multiplyDragByParticleVelocity = originalLimitVelocityOverLifetime.multiplyDragByParticleVelocity;

            // Copy inherit velocity module properties
            ParticleSystem.InheritVelocityModule originalInheritVelocity = original.inheritVelocity;
            ParticleSystem.InheritVelocityModule copyInheritVelocity = newEffect.inheritVelocity;
            copyInheritVelocity.enabled = originalInheritVelocity.enabled;
            copyInheritVelocity.mode = originalInheritVelocity.mode;
            copyInheritVelocity.curve = originalInheritVelocity.curve;
            copyInheritVelocity.curveMultiplier = originalInheritVelocity.curveMultiplier;

            // Copy force over lifetime module properties
            ParticleSystem.ForceOverLifetimeModule originalForceOverLifetime = original.forceOverLifetime;
            ParticleSystem.ForceOverLifetimeModule copyForceOverLifetime = newEffect.forceOverLifetime;
            copyForceOverLifetime.enabled = originalForceOverLifetime.enabled;
            copyForceOverLifetime.space = originalForceOverLifetime.space;
            copyForceOverLifetime.x = originalForceOverLifetime.x;
            copyForceOverLifetime.y = originalForceOverLifetime.y;
            copyForceOverLifetime.z = originalForceOverLifetime.z;
            copyForceOverLifetime.randomized = originalForceOverLifetime.randomized;
            copyForceOverLifetime.xMultiplier = originalForceOverLifetime.xMultiplier;
            copyForceOverLifetime.yMultiplier = originalForceOverLifetime.yMultiplier;
            copyForceOverLifetime.zMultiplier = originalForceOverLifetime.zMultiplier;

            // Copy color over lifetime module properties
            ParticleSystem.ColorOverLifetimeModule originalColorOverLifetime = original.colorOverLifetime;
            ParticleSystem.ColorOverLifetimeModule copyColorOverLifetime = newEffect.colorOverLifetime;
            copyColorOverLifetime.enabled = originalColorOverLifetime.enabled;
            copyColorOverLifetime.color = originalColorOverLifetime.color;

            // Copy color by speed module properties
            ParticleSystem.ColorBySpeedModule originalColorBySpeed = original.colorBySpeed;
            ParticleSystem.ColorBySpeedModule copyColorBySpeed = newEffect.colorBySpeed;
            copyColorBySpeed.enabled = originalColorBySpeed.enabled;
            copyColorBySpeed.color = originalColorBySpeed.color;
            copyColorBySpeed.range = originalColorBySpeed.range;

            // Copy size over lifetime module properties
            ParticleSystem.SizeOverLifetimeModule originalSizeOverLifetime = original.sizeOverLifetime;
            ParticleSystem.SizeOverLifetimeModule copySizeOverLifetime = newEffect.sizeOverLifetime;
            copySizeOverLifetime.enabled = originalSizeOverLifetime.enabled;
            copySizeOverLifetime.size = originalSizeOverLifetime.size;
            copySizeOverLifetime.separateAxes = originalSizeOverLifetime.separateAxes;
            copySizeOverLifetime.x = originalSizeOverLifetime.x;
            copySizeOverLifetime.y = originalSizeOverLifetime.y;
            copySizeOverLifetime.z = originalSizeOverLifetime.z;
            copySizeOverLifetime.sizeMultiplier = originalSizeOverLifetime.sizeMultiplier;
            copySizeOverLifetime.xMultiplier = originalSizeOverLifetime.xMultiplier;
            copySizeOverLifetime.yMultiplier = originalSizeOverLifetime.yMultiplier;
            copySizeOverLifetime.zMultiplier = originalSizeOverLifetime.zMultiplier;

            // Copy size by speed module properties
            ParticleSystem.SizeBySpeedModule originalSizeBySpeed = original.sizeBySpeed;
            ParticleSystem.SizeBySpeedModule copySizeBySpeed = newEffect.sizeBySpeed;
            copySizeBySpeed.enabled = originalSizeBySpeed.enabled;
            copySizeBySpeed.size = originalSizeBySpeed.size;
            copySizeBySpeed.separateAxes = originalSizeBySpeed.separateAxes;
            copySizeBySpeed.x = originalSizeBySpeed.x;
            copySizeBySpeed.y = originalSizeBySpeed.y;
            copySizeBySpeed.z = originalSizeBySpeed.z;
            copySizeBySpeed.range = originalSizeBySpeed.range;
            copySizeBySpeed.sizeMultiplier = originalSizeBySpeed.sizeMultiplier;
            copySizeBySpeed.xMultiplier = originalSizeBySpeed.xMultiplier;
            copySizeBySpeed.yMultiplier = originalSizeBySpeed.yMultiplier;
            copySizeBySpeed.zMultiplier = originalSizeBySpeed.zMultiplier;

            // Copy rotation over lifetime module properties
            ParticleSystem.RotationOverLifetimeModule originalRotationOverLifetime = original.rotationOverLifetime;
            ParticleSystem.RotationOverLifetimeModule copyRotationOverLifetime = newEffect.rotationOverLifetime;
            copyRotationOverLifetime.enabled = originalRotationOverLifetime.enabled;
            copyRotationOverLifetime.separateAxes = originalRotationOverLifetime.separateAxes;
            copyRotationOverLifetime.x = originalRotationOverLifetime.x;
            copyRotationOverLifetime.y = originalRotationOverLifetime.y;
            copyRotationOverLifetime.z = originalRotationOverLifetime.z;
            copyRotationOverLifetime.xMultiplier = originalRotationOverLifetime.xMultiplier;
            copyRotationOverLifetime.yMultiplier = originalRotationOverLifetime.yMultiplier;
            copyRotationOverLifetime.zMultiplier = originalRotationOverLifetime.zMultiplier;

            // Copy rotation by speed module properties
            ParticleSystem.RotationBySpeedModule originalRotationBySpeed = original.rotationBySpeed;
            ParticleSystem.RotationBySpeedModule copyRotationBySpeed = newEffect.rotationBySpeed;
            copyRotationBySpeed.enabled = originalRotationBySpeed.enabled;
            copyRotationBySpeed.separateAxes = originalRotationBySpeed.separateAxes;
            copyRotationBySpeed.x = originalRotationBySpeed.x;
            copyRotationBySpeed.y = originalRotationBySpeed.y;
            copyRotationBySpeed.z = originalRotationBySpeed.z;
            copyRotationBySpeed.range = originalRotationBySpeed.range;
            copyRotationBySpeed.xMultiplier = originalRotationBySpeed.xMultiplier;
            copyRotationBySpeed.yMultiplier = originalRotationBySpeed.yMultiplier;
            copyRotationBySpeed.zMultiplier = originalRotationBySpeed.zMultiplier;

            // Copy external forces module properties
            ParticleSystem.ExternalForcesModule originalExternalForces = original.externalForces;
            ParticleSystem.ExternalForcesModule copyExternalForces = newEffect.externalForces;
            copyExternalForces.enabled = originalExternalForces.enabled;
            copyExternalForces.multiplier = originalExternalForces.multiplier;
            copyExternalForces.multiplierCurve = originalExternalForces.multiplierCurve;
            copyExternalForces.influenceFilter = originalExternalForces.influenceFilter;
            copyExternalForces.influenceMask = originalExternalForces.influenceMask;
            if (originalExternalForces.influenceCount > 0)
            {
                for (int i = 0; i < originalExternalForces.influenceCount; i++)
                {
                    copyExternalForces.RemoveInfluence(i);
                    copyExternalForces.AddInfluence(originalExternalForces.GetInfluence(i));
                }
            }

            // Copy noise module properties
            ParticleSystem.NoiseModule originalNoise = original.noise;
            ParticleSystem.NoiseModule copyNoise = newEffect.noise;
            copyNoise.enabled = originalNoise.enabled;
            copyNoise.separateAxes = originalNoise.separateAxes;
            copyNoise.strength = originalNoise.strength;
            copyNoise.strengthMultiplier = originalNoise.strengthMultiplier;
            copyNoise.strengthX = originalNoise.strengthX;
            copyNoise.strengthY = originalNoise.strengthY;
            copyNoise.strengthZ = originalNoise.strengthZ;
            copyNoise.frequency = originalNoise.frequency;
            copyNoise.scrollSpeed = originalNoise.scrollSpeed;
            copyNoise.damping = originalNoise.damping;
            copyNoise.octaveCount = originalNoise.octaveCount;
            copyNoise.octaveMultiplier = originalNoise.octaveMultiplier;
            copyNoise.octaveScale = originalNoise.octaveScale;
            copyNoise.quality = originalNoise.quality;
            copyNoise.remap = originalNoise.remap;
            copyNoise.remapEnabled = originalNoise.remapEnabled;
            copyNoise.remapX = originalNoise.remapX;
            copyNoise.remapY = originalNoise.remapY;
            copyNoise.remapZ = originalNoise.remapZ;
            copyNoise.positionAmount = originalNoise.positionAmount;
            copyNoise.rotationAmount = originalNoise.rotationAmount;
            copyNoise.sizeAmount = originalNoise.sizeAmount;
            copyNoise.remapXMultiplier = originalNoise.remapXMultiplier;
            copyNoise.remapYMultiplier = originalNoise.remapYMultiplier;
            copyNoise.remapZMultiplier = originalNoise.remapZMultiplier;
            copyNoise.scrollSpeedMultiplier = originalNoise.scrollSpeedMultiplier;

            // Copy collision module properties
            ParticleSystem.CollisionModule originalCollision = original.collision;
            ParticleSystem.CollisionModule copyCollision = newEffect.collision;
            copyCollision.enabled = originalCollision.enabled;
            copyCollision.type = originalCollision.type;
            copyCollision.mode = originalCollision.mode;
            copyCollision.dampen = originalCollision.dampen;
            copyCollision.bounce = originalCollision.bounce;
            copyCollision.lifetimeLoss = originalCollision.lifetimeLoss;
            copyCollision.minKillSpeed = originalCollision.minKillSpeed;
            copyCollision.radiusScale = originalCollision.radiusScale;
            copyCollision.sendCollisionMessages = originalCollision.sendCollisionMessages;
            copyCollision.maxKillSpeed = originalCollision.maxKillSpeed;
            copyCollision.collidesWith = originalCollision.collidesWith;
            copyCollision.enableDynamicColliders = originalCollision.enableDynamicColliders;
            copyCollision.quality = originalCollision.quality;
            copyCollision.voxelSize = originalCollision.voxelSize;
            copyCollision.colliderForce = originalCollision.colliderForce;
            copyCollision.bounceMultiplier = originalCollision.bounceMultiplier;

            // Copy trigger module properties
            ParticleSystem.TriggerModule originalTrigger = original.trigger;
            ParticleSystem.TriggerModule copyTrigger = newEffect.trigger;
            copyTrigger.enabled = originalTrigger.enabled;
            copyTrigger.inside = originalTrigger.inside;
            copyTrigger.outside = originalTrigger.outside;
            copyTrigger.enter = originalTrigger.enter;
            copyTrigger.exit = originalTrigger.exit;
            copyTrigger.radiusScale = originalTrigger.radiusScale;
            copyTrigger.colliderQueryMode = originalTrigger.colliderQueryMode;
            if (originalTrigger.colliderCount > 0)
            {
                for (int i = 0; i < originalTrigger.colliderCount; i++)
                {
                    copyTrigger.RemoveCollider(i);
                    copyTrigger.AddCollider(originalTrigger.GetCollider(i));
                }
            }

            // Copy sub emitters module properties
            ParticleSystem.SubEmittersModule originalSubEmitters = original.subEmitters;
            ParticleSystem.SubEmittersModule copySubEmitters = newEffect.subEmitters;
            copySubEmitters.enabled = originalSubEmitters.enabled;
            if (originalSubEmitters.subEmittersCount > 0)
            {
                for (int i = 0; i < originalSubEmitters.subEmittersCount; i++)
                {
                    ParticleSystem originalSubEmitter = originalSubEmitters.GetSubEmitterSystem(i);
                    ParticleSystem copySubEmitter = new GameObject(originalSubEmitter.gameObject.name).AddComponent<ParticleSystem>();
                    originalSubEmitter.CopyInto(copySubEmitter);
                    copySubEmitters.RemoveSubEmitter(i);
                    copySubEmitters.AddSubEmitter(copySubEmitter, originalSubEmitters.GetSubEmitterType(i), originalSubEmitters.GetSubEmitterProperties(i));
                }
            }

            // Copy texture sheet animation module properties
            ParticleSystem.TextureSheetAnimationModule originalTextureSheetAnimation = original.textureSheetAnimation;
            ParticleSystem.TextureSheetAnimationModule copyTextureSheetAnimation = newEffect.textureSheetAnimation;
            copyTextureSheetAnimation.enabled = originalTextureSheetAnimation.enabled;
            copyTextureSheetAnimation.mode = originalTextureSheetAnimation.mode;
            copyTextureSheetAnimation.numTilesX = originalTextureSheetAnimation.numTilesX;
            copyTextureSheetAnimation.numTilesY = originalTextureSheetAnimation.numTilesY;
            copyTextureSheetAnimation.animation = originalTextureSheetAnimation.animation;
            copyTextureSheetAnimation.rowMode = originalTextureSheetAnimation.rowMode;
            copyTextureSheetAnimation.rowIndex = originalTextureSheetAnimation.rowIndex;
            copyTextureSheetAnimation.cycleCount = originalTextureSheetAnimation.cycleCount;
            copyTextureSheetAnimation.startFrame = originalTextureSheetAnimation.startFrame;
            copyTextureSheetAnimation.startFrameMultiplier = originalTextureSheetAnimation.startFrameMultiplier;
            copyTextureSheetAnimation.frameOverTime = originalTextureSheetAnimation.frameOverTime;
            copyTextureSheetAnimation.frameOverTimeMultiplier = originalTextureSheetAnimation.frameOverTimeMultiplier;
            copyTextureSheetAnimation.rowMode = originalTextureSheetAnimation.rowMode;
            copyTextureSheetAnimation.uvChannelMask = originalTextureSheetAnimation.uvChannelMask;
            copyTextureSheetAnimation.fps = originalTextureSheetAnimation.fps;
            copyTextureSheetAnimation.speedRange = originalTextureSheetAnimation.speedRange;
            copyTextureSheetAnimation.timeMode = originalTextureSheetAnimation.timeMode;
            if (originalTextureSheetAnimation.spriteCount > 0)
            {
                for (int i = 0; i < originalTextureSheetAnimation.spriteCount; i++)
                {
                    Sprite s = originalTextureSheetAnimation.GetSprite(i);
                    if (s == null) continue;
                    if (i < copyTextureSheetAnimation.spriteCount)
                    {
                        copyTextureSheetAnimation.SetSprite(i, s);
                    }
                    else copyTextureSheetAnimation.AddSprite(s);
                }
            }

            // Copy lights module properties
            ParticleSystem.LightsModule originalLights = original.lights;
            ParticleSystem.LightsModule copyLights = newEffect.lights;
            copyLights.enabled = originalLights.enabled;
            copyLights.intensity = originalLights.intensity;
            copyLights.intensityMultiplier = originalLights.intensityMultiplier;
            copyLights.range = originalLights.range;
            copyLights.useRandomDistribution = originalLights.useRandomDistribution;
            copyLights.sizeAffectsRange = originalLights.sizeAffectsRange;
            copyLights.alphaAffectsIntensity = originalLights.alphaAffectsIntensity;
            copyLights.ratio = originalLights.ratio;
            copyLights.useParticleColor = originalLights.useParticleColor;
            copyLights.maxLights = originalLights.maxLights;
            originalLights.light.CopyInto(copyLights.light);

            // Copy trail module properties
            ParticleSystem.TrailModule originalTrail = original.trails;
            ParticleSystem.TrailModule copyTrail = newEffect.trails;
            copyTrail.enabled = originalTrail.enabled;
            copyTrail.mode = originalTrail.mode;
            copyTrail.lifetime = originalTrail.lifetime;
            copyTrail.lifetimeMultiplier = originalTrail.lifetimeMultiplier;
            copyTrail.minVertexDistance = originalTrail.minVertexDistance;
            copyTrail.textureMode = originalTrail.textureMode;
            copyTrail.worldSpace = originalTrail.worldSpace;
            copyTrail.dieWithParticles = originalTrail.dieWithParticles;
            copyTrail.sizeAffectsWidth = originalTrail.sizeAffectsWidth;
            copyTrail.sizeAffectsLifetime = originalTrail.sizeAffectsLifetime;
            copyTrail.inheritParticleColor = originalTrail.inheritParticleColor;
            copyTrail.colorOverLifetime = originalTrail.colorOverLifetime;
            copyTrail.widthOverTrail = originalTrail.widthOverTrail;
            copyTrail.generateLightingData = originalTrail.generateLightingData;
            copyTrail.shadowBias = originalTrail.shadowBias;
            copyTrail.splitSubEmitterRibbons = originalTrail.splitSubEmitterRibbons;
            copyTrail.ratio = originalTrail.ratio;
            copyTrail.ribbonCount = originalTrail.ribbonCount;
            copyTrail.colorOverTrail = originalTrail.colorOverTrail;
            copyTrail.attachRibbonsToTransform = originalTrail.attachRibbonsToTransform;
            copyTrail.widthOverTrailMultiplier = originalTrail.widthOverTrailMultiplier;

            // Copy custom data module properties
            ParticleSystem.CustomDataModule originalCustomData = original.customData;
            ParticleSystem.CustomDataModule copyCustomData = newEffect.customData;
            copyCustomData.enabled = originalCustomData.enabled;
            for (int i = 0; i < 2; i++) // There are only two custom data modes: Custom1 and Custom2
            {
                ParticleSystemCustomData customData = (ParticleSystemCustomData) i;
                copyCustomData.SetMode(customData, originalCustomData.GetMode(customData));

                if (originalCustomData.GetMode(customData) == ParticleSystemCustomDataMode.Vector)
                {
                    int particleCount = original.particleCount;
                    for (int j = 0; j < particleCount; j++)
                    {
                        ParticleSystem.MinMaxCurve curve = originalCustomData.GetVector(customData, j);
                        copyCustomData.SetVector(customData, j, curve);
                    }
                }
                else if (originalCustomData.GetMode(customData) == ParticleSystemCustomDataMode.Color)
                {
                    ParticleSystem.MinMaxGradient gradient = originalCustomData.GetColor(customData);
                    copyCustomData.SetColor(customData, gradient);
                }
            }

            // Copy renderer module properties
            ParticleSystemRenderer originalRenderer = original.GetComponent<ParticleSystemRenderer>();
            ParticleSystemRenderer copyRenderer = newEffect.GetComponent<ParticleSystemRenderer>();
            copyRenderer.enabled = originalRenderer.enabled;
            copyRenderer.renderMode = originalRenderer.renderMode;
            copyRenderer.normalDirection = originalRenderer.normalDirection;
            copyRenderer.sortMode = originalRenderer.sortMode;
            copyRenderer.sortingFudge = originalRenderer.sortingFudge;
            copyRenderer.minParticleSize = originalRenderer.minParticleSize;
            copyRenderer.maxParticleSize = originalRenderer.maxParticleSize;
            copyRenderer.alignment = originalRenderer.alignment;
            copyRenderer.flip = originalRenderer.flip;
            copyRenderer.allowRoll = originalRenderer.allowRoll;
            copyRenderer.pivot = originalRenderer.pivot;
            copyRenderer.maskInteraction = originalRenderer.maskInteraction;
            copyRenderer.shadowCastingMode = originalRenderer.shadowCastingMode;
            copyRenderer.receiveShadows = originalRenderer.receiveShadows;
            copyRenderer.motionVectorGenerationMode = originalRenderer.motionVectorGenerationMode;
            copyRenderer.sortingLayerID = originalRenderer.sortingLayerID;
            copyRenderer.sortingOrder = originalRenderer.sortingOrder;
            copyRenderer.lightProbeUsage = originalRenderer.lightProbeUsage;
            copyRenderer.reflectionProbeUsage = originalRenderer.reflectionProbeUsage;
            copyRenderer.probeAnchor = originalRenderer.probeAnchor;
            copyRenderer.lightProbeProxyVolumeOverride = originalRenderer.lightProbeProxyVolumeOverride;
            copyRenderer.sharedMaterial = new Material(originalRenderer.sharedMaterial);
            copyRenderer.mesh = originalRenderer.mesh;
            copyRenderer.meshDistribution = originalRenderer.meshDistribution;
            copyRenderer.shadowBias = originalRenderer.shadowBias;
            copyRenderer.velocityScale = originalRenderer.velocityScale;
            copyRenderer.cameraVelocityScale = originalRenderer.cameraVelocityScale;
            copyRenderer.rotateWithStretchDirection = originalRenderer.rotateWithStretchDirection;
            copyRenderer.enableGPUInstancing = originalRenderer.enableGPUInstancing;
            copyRenderer.freeformStretching = originalRenderer.freeformStretching;
            copyRenderer.lengthScale = originalRenderer.lengthScale;
            if (originalRenderer.trailMaterial != null)
            {
                copyRenderer.trailMaterial = new Material(originalRenderer.trailMaterial);
            }
            Material[] originalMaterials = originalRenderer.sharedMaterials;
            Material[] copyMaterials = new Material[originalMaterials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                copyMaterials[i] = new Material(originalMaterials[i]);
            }

            copyRenderer.sharedMaterials = copyMaterials;
            Mesh[] meshes = new Mesh[originalRenderer.meshCount];
            originalRenderer.GetMeshes(meshes);
            copyRenderer.SetMeshes(meshes);
        }
    }
}