using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Controls the content of the Selection Menu Info Collumn.
    /// </summary>
    public class SelectionInfoColumn : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;
        
        
        
        [Serializable]
        public struct UIInfo
        {
            public Image previewImage;
        }
    }
}