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

        protected virtual void Awake()
        {
            overseer = CursorOverseerMono.GetInstance();
        }

        protected virtual void OnDisable()
        {
            overseer.Reset();
        }
        
        protected virtual void OnDestroy()
        {
            overseer.Reset();
        }

        public void OnPointerEnter(PointerEventData eventData) => overseer.Set(CursorToSet);
        public void OnPointerExit(PointerEventData eventData) => overseer.Set(CursorType.Default);

        protected abstract CursorType CursorToSet { get; }

    }
}