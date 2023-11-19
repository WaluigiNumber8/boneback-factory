using RedRats.Systems.LiteFeel.Core;
using RedRats.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates effects to a <see cref="Button"/>.
    /// </summary>
    public class LFSelectableBrain : MonoBehaviour
    {
        [SerializeField] private SelectableEventCaller eventCaller;
        [SerializeField] private LFEffector onClickEffector;
        [SerializeField] private LFEffector onSelectEffector;
        [SerializeField] private LFEffector onDeselectEffector;

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