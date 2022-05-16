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
    [RequireComponent(typeof(Image))]
    public class TabPageButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject page;
        [SerializeField] private EventInfo events;
        
        private TabGroup group;
        private Image background;

        private void Awake()
        {
            background = GetComponent<Image>();
            group = GetComponentInParent<TabGroup>();
            SafetyNet.EnsureIsNotNull(group, group.name);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            group.OnTabSelect(this);
            events.onClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            group.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            group.OnTabExit(this);
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