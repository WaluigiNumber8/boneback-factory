using System;
using Rogium.UserInterface.Editors.AssetSelection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Represents an asset in the form of a card.
    /// </summary>
    public class AssetCardControllerV2 : ToggleableBase
    {
        public static event Action<int> OnSelect;
        public static event Action<int> OnDeselect;
        
        [SerializeField] private UIInfo ui;

        protected int index;
        
        protected override void Awake()
        {
            base.Awake();
            toggle.onValueChanged.AddListener(CallSelectEvents);
        }

        public virtual void Construct(AssetCardData data)
        {
            index = data.index;
            if (ui.title != null) ui.title.text = data.title;
            if (ui.iconImage != null) ui.iconImage.sprite = data.icon;
        }
        
        private void CallSelectEvents(bool value)
        {
            if (value) OnSelect?.Invoke(index);
            else OnDeselect?.Invoke(index);
        }

        public string Title { get => ui.title.text; }
        public Sprite Icon {get => ui.iconImage.sprite;}

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image iconImage;
        }
    }
}