using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rogium.UserInterface.Editors.Utils
{
    
    /// <summary>
    /// Makes this UI element follow the pointer.
    /// </summary>
    public class UIPointerFollower : MonoBehaviour, IDragHandler
    {
        [SerializeField, EnumToggleButtons] private UIFollowType followType;
        [SerializeField] private bool rememberDraggedPosition;
        
        private RectTransform ttransform;
        private RectTransform canvasTransform;
        private InputSystem inputSystem;
        private Canvas canvas;
        private Camera cam;
        
        private Vector2 originalPosition;
        
        private void Awake()
        {
            ttransform = GetComponent<RectTransform>();
            inputSystem = InputSystem.GetInstance();
            canvas = GetComponentInParent<Canvas>();
            canvasTransform = canvas.GetComponent<RectTransform>();
            cam = Camera.main;
            
            originalPosition = ttransform.anchoredPosition;
        }

        private void OnEnable()
        {
            if (!rememberDraggedPosition) ttransform.anchoredPosition = originalPosition;
            if ((followType & UIFollowType.OnEnable) != 0 || (followType & UIFollowType.OnUpdate) != 0) MoveToPointerPosition();
            if ((followType & UIFollowType.OnUpdate) != 0) inputSystem.UI.PointerPosition.OnPressed += MoveToPointerPosition;
        }
        
        private void OnDisable()
        {
            if ((followType & UIFollowType.OnUpdate) != 0) inputSystem.UI.PointerPosition.OnPressed -= MoveToPointerPosition;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if ((followType & UIFollowType.OnDrag) != 0) DragBy(eventData.delta);
        }
        
        private void MoveToPointerPosition() => MoveToPointerPosition(inputSystem.PointerPosition);
        private void MoveToPointerPosition(Vector2 mousePosition)
        {
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, mousePosition, cam, out Vector2 newPos);
            
            newPos.x -= ttransform.rect.width  * 0.5f;
            newPos.y -= ttransform.rect.height * 0.5f;
            
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
            ttransform.localPosition = newPos;
        }

        private void DragBy(Vector2 delta)
        {
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions();
            Vector2 newPos = ttransform.anchoredPosition + (delta / canvas.scaleFactor);
            
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
            ttransform.anchoredPosition = newPos;
        }
        
        private (Vector2, Vector2) GetAllowedMinMaxPositions()
        {
            Vector2 minPos = canvasTransform.rect.min - ttransform.rect.min;
            Vector2 maxPos = canvasTransform.rect.max - ttransform.rect.max;
            return (minPos, maxPos);
        }
    }
}