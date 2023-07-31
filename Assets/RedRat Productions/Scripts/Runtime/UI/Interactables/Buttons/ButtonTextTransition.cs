using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.Core.Interactables.Buttons
{
    /// <summary>
    /// Updates Text Color based on button state.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonTextTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color highlighted = Color.white;
        [SerializeField] private Color pressed = Color.white;
        [SerializeField] private Color selected = Color.white;
        [SerializeField] private Color disabled = Color.white;

        private Color normal;
        private Button button;

        private bool isSelected;
        private bool isDisabled;
        
        private void Awake()
        {
            normal = text.color;
            button = GetComponent<Button>();
        }

        private void Start() => HandleSelectedStatus();

        private void Update()
        {
            HandleSelectedStatus();
            HandleDisabledStatus();
        }

        public void OnPointerEnter(PointerEventData eventData) => text.color = highlighted;
        public void OnPointerExit(PointerEventData eventData) => text.color = normal;
        public void OnPointerDown(PointerEventData eventData) => text.color = pressed;
        public void OnPointerUp(PointerEventData eventData) => text.color = highlighted;
        public void OnSelect(BaseEventData eventData)
        {
            text.color = selected;
            isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            text.color = normal;
            isSelected = false;
        }

        private void HandleSelectedStatus()
        {
            if (isSelected || EventSystem.current.currentSelectedGameObject != button.gameObject) return;
            isSelected = true;
            text.color = selected;
        }
        
        private void HandleDisabledStatus()
        {
            switch (button.interactable)
            {
                case false when !isDisabled:
                    isDisabled = true;
                    text.color = disabled;
                    return;
                
                case true when isDisabled:
                    isDisabled = false;
                    text.color = normal;
                    return;
            }
        }
    }
}