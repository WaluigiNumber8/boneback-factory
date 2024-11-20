using UnityEngine;
using UnityEngine.UI;

namespace RedRats.UI.Core.Scrolling
{
    /// <summary>
    /// Syncs multiple ScrollViews together.
    /// </summary>
    public class ScrollRectSyncer : MonoBehaviour
    {
        [SerializeField] private ScrollRect[] scrollViews;

        private void OnEnable()
        {
            foreach (ScrollRect scrollView in scrollViews)
            {
                scrollView.onValueChanged.AddListener(OnScroll);
            }
        }

        private void OnDisable()
        {
            foreach (ScrollRect scrollView in scrollViews)
            {
                scrollView.onValueChanged.RemoveListener(OnScroll);
            }
        }

        private void OnScroll(Vector2 value)
        {
            foreach (ScrollRect scrollView in scrollViews)
            {
                scrollView.normalizedPosition = value;
            }
        }    
    }
}