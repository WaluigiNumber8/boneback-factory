using RedRats.Systems.Effectors.Core;
using RedRats.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.Effectors.Brains
{
    /// <summary>
    /// Allocates effects to a <see cref="Button"/>.
    /// </summary>
    public class SelectableEffectsBrain : MonoBehaviour
    {
        [SerializeField] private SelectableEventCaller eventCaller;
        [SerializeField] private Effector onClickEffector;
        [SerializeField] private Effector onSelectEffector;
        [SerializeField] private Effector onDeselectEffector;

        private void OnEnable()
        {
            if (onClickEffector != null) eventCaller.OnClick += onClickEffector.Play;
            if (onSelectEffector != null) eventCaller.OnSelect += onSelectEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect += onDeselectEffector.Play;
        }

        private void OnDisable()
        {
            if (onClickEffector != null) eventCaller.OnClick -= onClickEffector.Play;
            if (onSelectEffector != null) eventCaller.OnSelect -= onSelectEffector.Play;
            if (onDeselectEffector != null) eventCaller.OnDeselect -= onDeselectEffector.Play;
        }
    }
}