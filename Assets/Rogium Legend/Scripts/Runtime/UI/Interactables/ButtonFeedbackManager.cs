using MoreMountains.Feedbacks;
using UnityEngine;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Associates <see cref="MMF_Player"/>s with a Ui component.
    /// </summary>
    public class ButtonFeedbackManager : MonoBehaviour
    {
        [SerializeField] private InteractableEventCaller button;
        [Space]
        [SerializeField] private MMF_Player onClickPlayer;
        [SerializeField] private MMF_Player onSelectPlayer;
        [SerializeField] private MMF_Player onDeselectPlayer;

        private void OnEnable()
        {
            button.OnClick += onClickPlayer.PlayFeedbacks;
            button.OnSelect += onSelectPlayer.PlayFeedbacks;
            button.OnDeselect += onDeselectPlayer.PlayFeedbacks;
        }

        private void OnDisable()
        {
            button.OnClick -= onClickPlayer.PlayFeedbacks;
            button.OnSelect -= onSelectPlayer.PlayFeedbacks;
            button.OnDeselect -= onDeselectPlayer.PlayFeedbacks;
        }

    }
}