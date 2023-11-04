using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.FeelExtension.Effectors
{
   /// <summary>
   /// The assigned <see cref="MMF_Player"/> is affected by the assigned <see cref="Toggle"/>.
   /// </summary>
    public class ToggleFeedbackEffector : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private MMF_Player turnOnFeedback;
        [SerializeField] private MMF_Player turnOffFeedback;

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
            if (value && turnOnFeedback != null) turnOnFeedback.PlayFeedbacks();
            if (!value && turnOffFeedback != null) turnOffFeedback.PlayFeedbacks();
        }
    }
}