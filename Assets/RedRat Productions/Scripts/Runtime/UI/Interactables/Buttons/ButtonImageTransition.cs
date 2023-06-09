using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Core.Interactables.Buttons
{
    /// <summary>
    /// Updates Image content based on button state.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonImageTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Image image;

        [FoldoutGroup("States")]
        [HorizontalGroup("States/State", marginRight:8), PreviewField(60), HideLabel] 
        [SerializeField] private Sprite normal, highlighted, pressed, selected, disabled;

        private Button button;

        private bool isSelected;
        private bool isDisabled;

        private void Awake() => button = GetComponent<Button>();

        private void Start()
        {
            image.sprite = normal;
            HandleSelectedStatus();
        }

        private void Update()
        {
            HandleSelectedStatus();
            HandleDisabledStatus();
        }

        public void OnPointerEnter(PointerEventData eventData) => image.sprite = highlighted;
        public void OnPointerExit(PointerEventData eventData) => image.sprite = normal;
        public void OnPointerDown(PointerEventData eventData) => image.sprite = pressed;
        public void OnPointerUp(PointerEventData eventData) => image.sprite = highlighted;

        public void OnSelect(BaseEventData eventData)
        {
            image.sprite = selected;
            isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            image.sprite = normal;
            isSelected = false;
        }

        private void HandleSelectedStatus()
        {
            if (isSelected || EventSystem.current.currentSelectedGameObject != button.gameObject) return;
            isSelected = true;
            image.sprite = selected;
        }

        private void HandleDisabledStatus()
        {
            switch (button.interactable)
            {
                case false when !isDisabled:
                    isDisabled = true;
                    image.sprite = disabled;
                    return;

                case true when isDisabled:
                    isDisabled = false;
                    image.sprite = normal;
                    return;
            }
        }
    }
}