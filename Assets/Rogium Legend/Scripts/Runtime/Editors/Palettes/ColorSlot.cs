using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Holds information about a given color slot from a palette.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ColorSlot : MonoBehaviour
    {
        public static event Action<int> OnSelectedAny;
        
        [SerializeField] private UIInfo ui;
        
        private Color currentColor;
        private Button button;
        private int index = -1;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(WhenSelected);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(WhenSelected);
        }

        /// <summary>
        /// Constructs the color slot.
        /// </summary>
        /// <param name="color">The new color it's going to carry.</param>
        /// <param name="index">The index of the color.</param>
        public void Construct(Color color, int index)
        {
            this.currentColor = color;
            this.index = index;
            RefreshUI();
        }

        /// <summary>
        /// Refreshes all the slots UI elements.
        /// </summary>
        private void RefreshUI()
        {
            ui.colorImg.color = currentColor;
        }

        private void WhenSelected()
        {
            OnSelectedAny?.Invoke(index);
        }
        
        public Color CurrentColor { get => currentColor; }
        public Image colorImage { get => ui.colorImg; }

        [Serializable]
        public struct UIInfo
        {
            public Image colorImg;
        }
    }
}