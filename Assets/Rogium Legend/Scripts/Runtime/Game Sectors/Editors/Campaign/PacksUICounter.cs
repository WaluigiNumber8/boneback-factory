using System;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
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

        private AssetSelector selector;
        private int counter;

        private void Awake() => selector = CampaignEditorOverseerMono.GetInstance().SelectionPicker.Selector;

        private void OnEnable()
        {
            selector.OnSelectCard += Increase;
            selector.OnDeselectCard += Decrease;
            CampaignEditorOverseer.Instance.OnAssignAsset += Reset;
        }

        private void OnDisable()
        {
            selector.OnSelectCard += Increase;
            selector.OnDeselectCard += Decrease;
            CampaignEditorOverseer.Instance.OnAssignAsset -= Reset;
        }

        private void Reset(IAsset asset)
        {
            counter = ((CampaignAsset)asset).PackReferences.Count;
            RefreshUI();
        }
        
        private void Increase(int _)
        {
            counter++;
            RefreshUI();
        }
        
        private void Decrease(int _)
        {
            counter--;
            RefreshUI();
        }

        private void RefreshUI() => counterText.text = counter.ToString();
        
        public int Counter { get => Convert.ToInt32(counterText.text); }
    }
}