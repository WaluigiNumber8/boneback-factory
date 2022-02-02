using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Contains Selection Menu Layout data.
    /// </summary>
    [System.Serializable]
    public class SelectionMenuLayout
    {
        [SerializeField] private Transform menu;
        [SerializeField] private Transform content;
        [SerializeField] private GameObject notFoundText;
        [SerializeField] private IconPositionType iconPositionType;
        [SerializeField] private Image iconPosition;

        public Transform Menu { get => menu; }
        public Transform Content { get => content; }
        public GameObject NotFoundText { get => notFoundText; }
        public IconPositionType IconPositionType { get => iconPositionType; }
        public Image IconPosition { get => iconPosition; }
    }
}