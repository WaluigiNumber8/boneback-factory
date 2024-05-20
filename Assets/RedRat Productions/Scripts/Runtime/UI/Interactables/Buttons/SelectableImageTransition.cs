using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core.Interactables.Buttons
{
    /// <summary>
    /// Updates Image content based on button state.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class SelectableImageTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Image image;
        [SerializeField, EnumToggleButtons] private TransitionAffectType affected = TransitionAffectType.Sprite;
        
        [FoldoutGroup("Sprite"), ShowIf("IsSpriteAffected")]
        [SerializeField, HorizontalGroup("Sprite/States", marginRight:8), PreviewField(60), HideLabel] 
        private Sprite normalSprite, highlightedSprite, pressedSprite, selectedSprite, disabledSprite;

        [SerializeField, FoldoutGroup("Color"), ShowIf("IsColorAffected")] private Color normal = Color.white;
        [SerializeField, FoldoutGroup("Color"), ShowIf("IsColorAffected")] private Color highlighted = Color.white;
        [SerializeField, FoldoutGroup("Color"), ShowIf("IsColorAffected")] private Color pressed = Color.white;
        [SerializeField, FoldoutGroup("Color"), ShowIf("IsColorAffected")] private Color selected = Color.white;
        [SerializeField, FoldoutGroup("Color"), ShowIf("IsColorAffected")] private Color disabled = Color.white;

        private Selectable selectable;

        private bool isSelected;
        private bool isDisabled;

        private void Awake() => selectable = GetComponent<Selectable>();

        private void Start()
        {
            Set(normalSprite, normal);
            HandleSelectedStatus();
        }

        private void Update()
        {
            HandleSelectedStatus();
            HandleDisabledStatus();
        }

        public void OnPointerEnter(PointerEventData eventData) => SetOnlyWhenEnabled(highlightedSprite, highlighted);
        public void OnPointerExit(PointerEventData eventData) => SetOnlyWhenEnabled(normalSprite, normal);
        public void OnPointerDown(PointerEventData eventData) => SetOnlyWhenEnabled(pressedSprite, pressed);
        public void OnPointerUp(PointerEventData eventData) => SetOnlyWhenEnabled(highlightedSprite, highlighted);

        public void OnSelect(BaseEventData eventData)
        {
            SetOnlyWhenEnabled(selectedSprite, selected);
            isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            SetOnlyWhenEnabled(normalSprite, normal);
            isSelected = false;
        }

        private void HandleSelectedStatus()
        {
            if (!selectable.interactable) return;
            if (isSelected || EventSystem.current.currentSelectedGameObject != selectable.gameObject) return;
            isSelected = true;
            Set(selectedSprite, selected);
        }

        private void HandleDisabledStatus()
        {
            switch (selectable.interactable)
            {
                case false when !isDisabled:
                    isDisabled = true;
                    Set(disabledSprite, disabled);
                    return;

                case true when isDisabled:
                    isDisabled = false;
                    Set(normalSprite, normal);
                    return;
            }
        }
        
        private void SetOnlyWhenEnabled(Sprite sprite, Color color)
        {
            if (!selectable.interactable) return;
            Set(sprite, color);
        }
        
        private void Set(Sprite sprite, Color color)
        {
            if (IsSpriteAffected()) image.sprite = sprite;
            if (IsColorAffected()) image.color = color;
        }
        
        private bool IsSpriteAffected() => (affected & TransitionAffectType.Sprite) != 0;
        private bool IsColorAffected() => (affected & TransitionAffectType.Color) != 0;
    }
}