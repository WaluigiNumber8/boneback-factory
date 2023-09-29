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
            if (onClickPlayer != null) interactable.OnClick += onClickPlayer.PlayFeedbacks;
            if (onSelectPlayer != null) interactable.OnSelect += onSelectPlayer.PlayFeedbacks;
            if (onDeselectPlayer != null) interactable.OnDeselect += onDeselectPlayer.PlayFeedbacks;
        }

        private void OnDisable()
        {
            if (onClickPlayer != null) interactable.OnClick -= onClickPlayer.PlayFeedbacks;
            if (onSelectPlayer != null) interactable.OnSelect -= onSelectPlayer.PlayFeedbacks;
            if (onDeselectPlayer != null) interactable.OnDeselect -= onDeselectPlayer.PlayFeedbacks;
        }

    }
}