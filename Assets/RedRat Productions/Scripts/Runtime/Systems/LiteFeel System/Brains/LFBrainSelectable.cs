using RedRats.Systems.LiteFeel.Core;
using RedRats.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates effects to a <see cref="Button"/>.
    /// </summary>
    public class LFBrainSelectable : MonoBehaviour
    {
        [SerializeField] private SelectableEventCaller eventCaller;
        [Space]
        [SerializeField] private LFEffector onSelectEffector;
        [SerializeField] private LFEffector onDeselectEffector;
        [Space]
        [SerializeField] private LFEffector onClickEffector;
        [SerializeField] private LFEffector onClickUpEffector;
        [SerializeField] private LFEffector onClickDownEffector;

        private void OnEnable()
        {
            if (onSelectEffector != null) eventCaller.OnSelect += onSelectEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect += onDeselectEffector.Play;
            if (onClickEffector != null) eventCaller.OnClick += onClickEffector.Play;
            if (onClickUpEffector != null) eventCaller.OnClickUp += onClickUpEffector.Play;
            if (onClickDownEffector != null) eventCaller.OnClickDown += onClickDownEffector.Play;
        }

        private void OnDisable()
        {
            if (onClickEffector != null) eventCaller.OnClick -= onClickEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect -= onDeselectEffector.Play;
            if (onSelectEffector != null) eventCaller.OnSelect -= onSelectEffector.Play;
            if (onClickUpEffector != null) eventCaller.OnClickUp -= onClickUpEffector.Play;
            if (onClickDownEffector != null) eventCaller.OnClickDown -= onClickDownEffector.Play;
        }
    }
}