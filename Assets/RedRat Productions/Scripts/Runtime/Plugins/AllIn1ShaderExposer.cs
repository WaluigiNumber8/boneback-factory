using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Plugins
{
    /// <summary>
    /// Exposes the properties of an AllIn1Shader material (for animating).
    /// </summary>
    public class AllIn1ShaderExposer : MonoBehaviour
    {
        private static readonly int AttributeShineLocation = Shader.PropertyToID("_ShineLocation");
        
        [SerializeField, HideIf("meshRenderer")] private Image image;
        [SerializeField, HideIf("image")] private MeshRenderer meshRenderer;
        
        [HideInInspector] public float shineLocation;
        
        private Material material;

        private void Awake()
        {
            material = (image != null) ? image.material = new Material(image.material) :
                (meshRenderer != null) ? meshRenderer.material :
                throw new MissingReferenceException("You must assign an Image or MeshRenderer Component for animating.");
        }

        private void Update()
        {
            material.SetFloat(AttributeShineLocation, shineLocation);
        }
    }
}