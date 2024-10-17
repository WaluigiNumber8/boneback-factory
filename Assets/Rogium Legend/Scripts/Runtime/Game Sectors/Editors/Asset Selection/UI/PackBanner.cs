using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Editors.NewAssetSelection.UI
{
    /// <summary>
    /// Controls everything happening on the Navigation Bar. Everything from what buttons do what, to what shows up.
    /// </summary>
    public class PackBanner : MonoBehaviour
    {
        [SerializeField] private NavBarUIInfo ui;
        
        [Serializable]
        public struct NavBarUIInfo
        {
            public TextMeshProUGUI packTitleText;
            public Image packIcon;
            public Button configButton;
        }
    }
}