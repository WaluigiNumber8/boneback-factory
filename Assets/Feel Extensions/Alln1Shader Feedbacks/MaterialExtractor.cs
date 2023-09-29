using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Plugins
{
    /// <summary>
    /// Grabs a material from the current object.
    /// </summary>
    public class MaterialExtractor : MonoBehaviour
    {
        private Material material;

        private void Awake()
        {
            Image image = GetComponent<Image>();
            Renderer render = GetComponent<Renderer>();
            
            material = (image != null) ? image.material = new Material(image.material) :
                (render != null) ? render.material :
                throw new MissingReferenceException("You must assign an Image or MeshRenderer Component for animating.");
        }

        /// <summary>
        /// Returns the extracted material.
        /// </summary>
        /// <returns></returns>
        public Material Get() => material;

    }
}