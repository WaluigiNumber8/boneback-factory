using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Core.Utils
{
    /// <summary>
    /// Grabs a material from the current object.
    /// </summary>
    public class MaterialExtractor : MonoBehaviour
    {
        [InfoBox("If you're getting material being null, assign the Image or MeshRenderer manually here.", InfoMessageType.Warning)]
        [SerializeField, HideIf("assignedImage")] private Renderer assignedRenderer;
        [SerializeField, HideIf("assignedRenderer")] private Image assignedImage;
        
        private Material material;

        private void Awake()
        {
            Image image = (assignedImage == null) ? GetComponent<Image>() : assignedImage;
            Renderer render = (assignedRenderer == null) ? GetComponent<Renderer>() : assignedRenderer;
            
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