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

        private RectTransform ttransform;
        private InputSystem inputSystem;
        private Canvas canvas;
        private RectTransform canvasTransform;
        private Camera cam;
        
        private void Awake()
        {
            ttransform = GetComponent<RectTransform>();
            inputSystem = InputSystem.GetInstance();
            canvas = GetComponentInParent<Canvas>();
            canvasTransform = canvas.GetComponent<RectTransform>();
            cam = Camera.main;
        }

        private void OnEnable()
        {
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
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, mousePosition, cam, out Vector2 newPos);
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions();
            
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
            ttransform.localPosition = newPos;
        }

        private void DragBy(Vector2 delta)
        {
            Vector2 newPos = ttransform.anchoredPosition + (delta / canvas.scaleFactor);
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions();
            
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