using System.Collections;
using RedRats.UI.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// General utility methods for working with Modal Windows.
    /// </summary>
    public static class TUtilsModalWindow
    {
        public static IEnumerator WindowAccept()
        {
            Object.FindFirstObjectByType<ModalWindow>().OnAccept();
            yield return null;
        } 
        
        public static IEnumerator WindowCancel()
        {
            Object.FindFirstObjectByType<ModalWindow>().OnDeny();
            yield return null;
        }

        public static IEnumerator WindowSpecial()
        {
            Object.FindFirstObjectByType<ModalWindow>().OnSpecial();
            yield return null;
        }

        public static bool IsModalWindowActive()
        {
            ModalWindow window = Object.FindFirstObjectByType<ModalWindow>();
            return (window != null && window.transform.GetChild(0).gameObject.activeSelf);
        }
    }
}