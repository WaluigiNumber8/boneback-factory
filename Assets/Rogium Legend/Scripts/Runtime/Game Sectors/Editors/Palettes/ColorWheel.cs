using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Picks a color from a texture.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ColorWheel : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        public event Action<Color> OnValueChanged;

        [SerializeField] private Color color;
        [SerializeField] private RectTransform positionIndicator;
        
        private Camera cam;
        private Image pickerImage;
        private RectTransform imageTransform;
        private Texture2D tex;

        private void Awake()
        {
            cam = Camera.main;
            pickerImage = GetComponent<Image>();
            imageTransform = pickerImage.rectTransform;
            tex = pickerImage.sprite.texture;
        }
        
        public void OnPointerClick(PointerEventData eventData) => WhenColorPick(eventData.position);
        public void OnDrag(PointerEventData eventData) => WhenColorPick(eventData.position);

        private void WhenColorPick(Vector2 mousePosition)
        {
            color = Pick(mousePosition);
            if (color.a == 0) return;
            MoveIndicatorToPointer(mousePosition);
            OnValueChanged?.Invoke(color);
        }
        
        /// <summary>
        /// Picks a color from a screen position on the image.
        /// </summary>
        /// <param name="screenPosition">The screen position on the image.</param>
        /// <returns>The color of that pixel.</returns>
        private Color Pick(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(imageTransform, screenPosition, cam, out Vector2 localPoint)) return Color.white;
            
            // Convert local coordinates to texture coordinates
            Vector2 normalizedPoint = Rect.PointToNormalized(imageTransform.rect, localPoint);
            int x = Mathf.FloorToInt(normalizedPoint.x * tex.width);
            int y = Mathf.FloorToInt(normalizedPoint.y * tex.height);
            return tex.GetPixel(x, y);
        }
        
        private void MoveIndicatorToPointer(Vector2 mousePosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(imageTransform, mousePosition, cam, out Vector2 newPos);
            
            newPos.x = Mathf.Clamp(newPos.x, imageTransform.rect.x, imageTransform.rect.x + imageTransform.rect.width);
            newPos.y = Mathf.Clamp(newPos.y, imageTransform.rect.y, imageTransform.rect.y + imageTransform.rect.height);
            positionIndicator.localPosition = newPos;
        }

    }
}