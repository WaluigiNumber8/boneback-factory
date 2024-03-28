using RedRats.Systems.LiteFeel.Core;
using RedRats.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a <see cref="Selectable"/>.
    /// </summary>
    public class LFBrainSelectable : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private SelectableEventCaller eventCaller;
        [Space]
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private LFEffector onSelectEffector;
        [SerializeField, GUIColor(0.1f, 0.75f, 0f)] private LFEffector onDeselectEffector;
        [Space]
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickLeftEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickRightEffector;
        [Space]
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickLeftDownEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickLeftUpEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickRightDownEffector;
        [SerializeField, GUIColor(0f, 0.75f, 1f)] private LFEffector onClickRightUpEffector;

        private void OnEnable()
        {
            if (onSelectEffector != null) eventCaller.OnSelect += onSelectEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect += onDeselectEffector.Play;
            if (onClickLeftEffector != null) eventCaller.OnClickLeft += onClickLeftEffector.Play;
            if (onClickRightEffector != null) eventCaller.OnClickRight += onClickRightEffector.Play;
            if (onClickLeftUpEffector != null) eventCaller.OnClickLeftUp += onClickLeftUpEffector.Play;
            if (onClickLeftDownEffector != null) eventCaller.OnClickLeftDown += onClickLeftDownEffector.Play;
            if (onClickRightDownEffector != null) eventCaller.OnClickRightDown += onClickRightDownEffector.Play;
            if (onClickRightUpEffector != null) eventCaller.OnClickRightUp += onClickRightUpEffector.Play;
        }

        private void OnDisable()
        {
            if (onClickLeftEffector != null) eventCaller.OnClickLeft -= onClickLeftEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect -= onDeselectEffector.Play;
            if (onSelectEffector != null) eventCaller.OnSelect -= onSelectEffector.Play;
            if (onClickRightEffector != null) eventCaller.OnClickRight -= onClickRightEffector.Play;
            if (onClickLeftUpEffector != null) eventCaller.OnClickLeftUp -= onClickLeftUpEffector.Play;
            if (onClickLeftDownEffector != null) eventCaller.OnClickLeftDown -= onClickLeftDownEffector.Play;
            if (onClickRightDownEffector != null) eventCaller.OnClickRightDown -= onClickRightDownEffector.Play;
            if (onClickRightUpEffector != null) eventCaller.OnClickRightUp -= onClickRightUpEffector.Play;
        }
    }
}