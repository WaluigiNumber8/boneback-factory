using UnityEngine;

namespace RogiumLegend.Global.MenuSystem.AssetSelection
{
    /// <summary>
    /// Contains Selection Menu Layout data.
    /// </summary>
    [System.Serializable]
    public class SelectionMenuLayout
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject content;

        public GameObject Menu { get => menu; }
        public GameObject Content { get => content; }
    }
}