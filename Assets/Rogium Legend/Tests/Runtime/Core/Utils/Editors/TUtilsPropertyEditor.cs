using System.Collections;
using Rogium.Editors.PropertyEditor;
using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Utility methods for property editor tests.
    /// </summary>
    public static class TUtilsPropertyEditor
    {
        public static IEnumerator EditFirstInputField(string newValue)
        {
            IPInputField inputField = PropertyEditorOverseerMono.Instance.GetComponentInChildren<IPInputField>();
            inputField.UpdateValue(newValue);
            yield return null;
        }
    }
}