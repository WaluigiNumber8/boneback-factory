using System;
using System.Collections;
using Rogium.Editors.AssetSelection;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Tracks the amount of packs selected and updates it in the UI.
    /// </summary>
    public class PacksUICounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private AssetSelectionPickerBase picker;
        private int counter;

        private void Awake() => picker = CampaignEditorOverseerMono.GetInstance().SelectionPicker;

        private void OnEnable()
        {
            picker.Selector.OnSelectCard += RefreshCounter;
            picker.Selector.OnDeselectCard += RefreshCounter;
            CampaignEditorOverseer.Instance.OnAssignAsset += RefreshCounter;
        }

        private void OnDisable()
        {
            picker.Selector.OnSelectCard += RefreshCounter;
            picker.Selector.OnDeselectCard += RefreshCounter;
            CampaignEditorOverseer.Instance.OnAssignAsset -= RefreshCounter;
        }

        private void RefreshCounter(CampaignAsset obj)
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return null;
                RefreshCounter(0);
            }
        }

        private void RefreshCounter(int _) => counterText.text = picker.SelectedAssetsCount.ToString();

        public int Counter { get => Convert.ToInt32(counterText.text); }
    }
}