using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.Core.Interactables.bs
{
    /// <summary>
    /// Updates Text Color based on button state.
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class SelectableTextTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color highlighted = Color.white;
        [SerializeField] private Color pressed = Color.white;
        [SerializeField] private Color selected = Color.white;
        [SerializeField] private Color disabled = Color.white;

        private Color normal;
        private Selectable selectable;

        private bool isSelected;
        private bool isDisabled;
        
        private void Awake()
        {
            normal = text.color;
            selectable = GetComponent<Selectable>();
        }

        private void Start() => HandleSelectedStatus();

        private void Update()
        {
            HandleSelectedStatus();
            HandleDisabledStatus();
        }

        public void OnPointerEnter(PointerEventData eventData) => SetOnlyWhenEnabled(highlighted);
        public void OnPointerExit(PointerEventData eventData) => SetOnlyWhenEnabled(normal);
        public void OnPointerDown(PointerEventData eventData) => SetOnlyWhenEnabled(pressed);
        public void OnPointerUp(PointerEventData eventData) => SetOnlyWhenEnabled(highlighted);
        public void OnSelect(BaseEventData eventData)
        {
            SetOnlyWhenEnabled(selected);
            isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            SetOnlyWhenEnabled(normal);
            isSelected = false;
        }

        private void HandleSelectedStatus()
        {
            if (!selectable.interactable) return;
            if (isSelected || EventSystem.current.currentSelectedGameObject != selectable.gameObject) return;
            isSelected = true;
            text.color = selected;
        }
        
        private void HandleDisabledStatus()
        {
            switch (selectable.interactable)
            {
                case false when !isDisabled:
                    isDisabled = true;
                    Set(disabled);
                    return;
                
                case true when isDisabled:
                    isDisabled = false;
                    Set(normal);
                    return;
            }
        }
        
        private void SetOnlyWhenEnabled(Color color)
        {
            if (!selectable.interactable) return;
            Set(color);
        }

        private void Set(Color color) => text.color = color;
    }
}