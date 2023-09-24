using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Plugins
{
    /// <summary>
    /// Grabs a material from the current object.
    /// </summary>
    public class MaterialExtractor : MonoBehaviour
    {
        [SerializeField, HideIf("meshRenderer")] private Image image;
        [SerializeField, HideIf("image")] private MeshRenderer meshRenderer;
        
        private Material material;

        private void Awake()
        {
            material = (image != null) ? image.material = new Material(image.material) :
                (meshRenderer != null) ? meshRenderer.material :
                throw new MissingReferenceException("You must assign an Image or MeshRenderer Component for animating.");
        }

        /// <summary>
        /// Returns the extracted material.
        /// </summary>
        /// <returns></returns>
        public Material Get() => material;

    }
}