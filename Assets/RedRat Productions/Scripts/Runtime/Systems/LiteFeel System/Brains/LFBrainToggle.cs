using System.Collections;
using RedRats.Systems.LiteFeel.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a <see cref="Toggle"/>.
    /// </summary>
    public class LFBrainToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private LFEffector turnOnEffect;
        [SerializeField] private LFEffector turnOffEffect;
        
        private void OnEnable()
        {
            StartCoroutine(EnableCoroutine());
            IEnumerator EnableCoroutine()
            {
                yield return new WaitForSeconds(0.01f);
                toggle.onValueChanged.AddListener(WhenValueChange);
            }
        }

        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenValueChange);
        
        private void WhenValueChange(bool value)
        {
            if (value && turnOnEffect != null) turnOnEffect.Play();
            if (!value && turnOffEffect != null) turnOffEffect.Play();
        }
    }
}