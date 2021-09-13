using UnityEngine;
using UnityEngine.UI;

namespace RogiumLegend.Global.UISystem.AssetSelection
{
    /// <summary>
    /// Contains Selection Menu Layout data.
    /// </summary>
    [System.Serializable]
    public class SelectionMenuLayout
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject content;
        [SerializeField] private IconPositionType iconPositionType;
        [SerializeField] private Image iconPosition;

        public GameObject Menu { get => menu; }
        public GameObject Content { get => content; }
        public IconPositionType IconPositionType { get => iconPositionType; }
        public Image IconPosition { get => iconPosition; }
    }
}