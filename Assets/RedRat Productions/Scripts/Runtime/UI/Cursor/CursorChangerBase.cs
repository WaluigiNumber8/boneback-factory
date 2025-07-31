using UnityEngine;
using UnityEngine.EventSystems;

namespace RedRats.UI.Core.Cursors
{
    /// <summary>
    /// A base for all cursor changers.
    /// </summary>
    public abstract class CursorChangerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CursorOverseerMono overseer;
        private bool withinBounds;
        
        protected virtual void Awake() => overseer = CursorOverseerMono.Instance;

        protected virtual void OnDisable()
        {
            overseer.SetDefault();
            withinBounds = false;
        }
        
        protected virtual void OnDestroy()
        {
            overseer.SetDefault();
            withinBounds = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            overseer.Set(CursorToSet);
            withinBounds = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            overseer.Set(CursorType.Default);
            withinBounds = false;
        }

        protected void SetIfWithinBounds()
        {
            if (!withinBounds) return;
            overseer.Set(CursorToSet);
        }
        
        protected abstract CursorType CursorToSet { get; }

    }
}