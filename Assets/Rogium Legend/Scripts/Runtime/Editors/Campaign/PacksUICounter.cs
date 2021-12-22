using Rogium.Editors.Core;
using Rogium.UserInterface.AssetSelection;
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

        private int counter;

        private void OnEnable()
        {
            AssetCardPickerController.OnSelected += WhenCounterIncrease;
            AssetCardPickerController.OnDeselected += WhenCounterDecrease;
            CampaignEditorOverseer.Instance.OnAssignAsset += ResetCounter;
        }

        private void OnDisable()
        {
            AssetCardPickerController.OnSelected -= WhenCounterIncrease;
            AssetCardPickerController.OnDeselected -= WhenCounterDecrease;
            CampaignEditorOverseer.Instance.OnAssignAsset -= ResetCounter;
        }

        /// <summary>
        /// Sets the counter to 0.
        /// </summary>
        private void ResetCounter(AssetBase asset)
        {
            counter = 0;
            RefreshUI();
        }
        
        /// <summary>
        /// Increases the counter by 1.
        /// </summary>
        /// <param name="asset"></param>
        private void WhenCounterIncrease(AssetBase asset)
        {
            counter++;
            RefreshUI();
        }
        
        /// <summary>
        /// Decreases the Counter by 1.
        /// </summary>
        /// <param name="asset"></param>
        private void WhenCounterDecrease(AssetBase asset)
        {
            counter--;
            RefreshUI();
        }

        /// <summary>
        /// Refreshes the User Interface.
        /// </summary>
        private void RefreshUI()
        {
            counterText.text = counter.ToString();
        }
    }
}