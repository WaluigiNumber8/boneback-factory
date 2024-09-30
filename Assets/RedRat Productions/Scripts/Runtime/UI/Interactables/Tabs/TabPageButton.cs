using RedRats.Safety;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRats.UI.Tabs
{
    /// <summary>
    /// A Button, belonging to a TabGroup, located on one of the parents of this object.
    /// </summary>
    public class TabPageButton : Button
    {
        [SerializeField] private Image background;
        [SerializeField] private GameObject page;
        [SerializeField] private EventInfo events;
        
        private TabGroup group;

        protected override void Awake()
        {
            group = GetComponentInParent<TabGroup>();
            SafetyNet.EnsureIsNotNull(group, group.name);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            group.OnTabSelect(this);
            events.onClick?.Invoke();
            base.OnPointerClick(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            group.OnTabEnter(this);
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            group.OnTabExit(this);
            base.OnPointerExit(eventData);
        }

        public Image Background { get => background; }
        public GameObject Page { get => page; }

        [System.Serializable]
        public struct EventInfo
        {
            public UnityEvent onClick;
        }
    }
}