using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.FeelExtension
{
    /// <summary>
    /// Associates <see cref="MMF_Player"/>s with a UI component.
    /// </summary>
    public class InteractableFeedbackManager : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] protected InteractableEventCaller interactable;
        [Space]
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private MMF_Player onSelectPlayer;
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private MMF_Player onDeselectPlayer;
        [Space]
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private MMF_Player onClickPlayer;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private MMF_Player onClickDownPlayer;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private MMF_Player onClickUpPlayer;
        

        protected virtual void OnEnable()
        {
            if (onSelectPlayer != null) interactable.OnSelect += onSelectPlayer.PlayFeedbacks;
            if (onDeselectPlayer != null) interactable.OnDeselect += onDeselectPlayer.PlayFeedbacks;
            if (onClickPlayer != null) interactable.OnClick += onClickPlayer.PlayFeedbacks;
            if (onClickDownPlayer != null) interactable.OnClickDown += onClickDownPlayer.PlayFeedbacks;
            if (onClickUpPlayer != null) interactable.OnClickUp += onClickUpPlayer.PlayFeedbacks;
        }

        protected virtual void OnDisable()
        {
            if (onClickPlayer != null) interactable.OnClick -= onClickPlayer.PlayFeedbacks;
            if (onSelectPlayer != null) interactable.OnSelect -= onSelectPlayer.PlayFeedbacks;
            if (onDeselectPlayer != null) interactable.OnDeselect -= onDeselectPlayer.PlayFeedbacks;
            if (onClickDownPlayer != null) interactable.OnClickDown -= onClickDownPlayer.PlayFeedbacks;
            if (onClickUpPlayer != null) interactable.OnClickUp -= onClickUpPlayer.PlayFeedbacks;
        }
    }
}