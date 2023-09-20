using MoreMountains.Feedbacks;
using UnityEngine;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Associates <see cref="MMF_Player"/>s with a UI component.
    /// </summary>
    public class InteractableFeedbackManager : MonoBehaviour
    {
        [SerializeField] private InteractableEventCaller interactable;
        [Space]
        [SerializeField] private MMF_Player onClickPlayer;
        [SerializeField] private MMF_Player onSelectPlayer;
        [SerializeField] private MMF_Player onDeselectPlayer;

        private void OnEnable()
        {
            interactable.OnClick += onClickPlayer.PlayFeedbacks;
            interactable.OnSelect += onSelectPlayer.PlayFeedbacks;
            interactable.OnDeselect += onDeselectPlayer.PlayFeedbacks;
        }

        private void OnDisable()
        {
            interactable.OnClick -= onClickPlayer.PlayFeedbacks;
            interactable.OnSelect -= onSelectPlayer.PlayFeedbacks;
            interactable.OnDeselect -= onDeselectPlayer.PlayFeedbacks;
        }

    }
}