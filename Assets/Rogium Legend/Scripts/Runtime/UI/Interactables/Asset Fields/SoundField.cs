using System;
using RedRats.Systems.Audio;
using RedRats.UI.Core.Interactables.Buttons;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sounds;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Audio;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Stores <see cref="AssetData"/> for sounds.
    /// </summary>
    public class SoundField : MonoBehaviour, IAssetField<AssetData>, IPointerClickHandler
    {
        public event Action<SoundAsset> OnSoundChanged;
        public event Action<AssetData> OnValueChanged;
        public event Action OnValueEmptied;
        
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private UIInfo ui;
        
        private AssetData value;
        private AssetData lastValue;
        private SoundAsset asset;
        private bool canBeEmpty;
        
        private void Awake()
        {
            ui.showWindowButton.onClick.AddListener(() => ModalWindowBuilder.GetInstance().OpenSoundPickerWindow(UpdateSoundAsset, UpdateValue, value));
            ui.playButton.onClick.AddListener(() => AudioSystemRogium.GetInstance().PlaySound(value, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false)));
            ui.showWindowButton.OnClickRight += EmptyOut;
            ui.playButton.OnClickRight += EmptyOut;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!canBeEmpty || eventData.button != PointerEventData.InputButton.Right) return;
            EmptyOut();
        }
        
        public void Construct(AssetData value, bool canBeEmpty = false)
        {
            this.value = value;
            this.lastValue = value;
            this.canBeEmpty = canBeEmpty;
            
            if (value.IsEmpty()) { ClearElements(); return; }
            Refresh(InternalLibraryOverseer.GetInstance().GetSoundByID(value.ID));
        }

        public void SetActive(bool isActive)
        {
            ui.showWindowButton.interactable = isActive;
            ui.playButton.interactable = isActive;
        }
        
        public void UpdateValue(AssetData value)
        {
            this.lastValue = this.value;
            this.value = value;
            OnValueChanged?.Invoke(value);
            
        }
        
        public void UpdateSoundAsset(SoundAsset asset)
        {
            if (asset != null) value = new AssetData(asset.ID, value.Parameters);
            this.asset = asset;
            Refresh(asset);
            OnSoundChanged?.Invoke(asset);
        }
        
        private void Refresh(IAsset newAsset)
        {
            if (newAsset.IsEmpty())
            {
                ClearElements();
                return;
            }
            
            ui.soundTitle.text = newAsset.Title;
            ui.soundIcon.sprite = newAsset.Icon;
            ui.soundIcon.color = Color.white;
        }

        private void EmptyOut()
        {
            Clear();
            ActionHistorySystem.AddAndExecute(new UpdateSoundFieldAction(this, new AssetData(ParameterInfoConstants.ForSound), value, null, asset)); 
        }
        
        private void Clear()
        {
            value = new AssetData(ParameterInfoConstants.ForSound);
            ClearElements();
            OnValueEmptied?.Invoke();
        }
        
        private void ClearElements()
        {
            ui.soundTitle.text = "None";
            ui.soundIcon.color = Color.clear;
        }
        
        public AssetData Value { get => value; }
        public UIInfo UI { get => ui; }
        
        [Serializable]
        public struct UIInfo
        {
            public EnhancedButton showWindowButton;
            public EnhancedButton playButton;
            public TextMeshProUGUI soundTitle;
            public Image soundIcon;
            public Image playSoundButtonIcon;
        }
    }
}