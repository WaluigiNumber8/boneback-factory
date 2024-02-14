using RedRats.Systems.LiteFeel.Core;
using RedRats.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates effects to a <see cref="Button"/>.
    /// </summary>
    public class LFBrainSelectable : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private SelectableEventCaller eventCaller;
        [Space]
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private LFEffector onSelectEffector;
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private LFEffector onDeselectEffector;
        [Space]
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickUpEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickDownEffector;

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