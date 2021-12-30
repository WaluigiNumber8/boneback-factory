using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoubakProductions.UI.Toggles
{
    /// <summary>
    /// Makes a toggle change a value of text.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class TextToggleProperty : MonoBehaviour
    {
        [SerializeField] private string onText = "On";
        [SerializeField] private string offText = "Off";
        [Space]
        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(ChangeText);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(ChangeText);
        }

        /// <summary>
        /// Changes the text based on toggle's value.
        /// </summary>
        /// <param name="value">The value of the toggle.</param>
        private void ChangeText(bool value)
        {
            text.text = (value) ? onText : offText;
        }
        
    }
}