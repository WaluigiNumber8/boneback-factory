using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.Properties.InteractablesCreator;
using static Rogium.Tests.UI.Interactables.Properties.InteractableUtils;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Tests for the <see cref="ColorField"/> interactable property.
    /// </summary>
    public class IPColorFieldTests : MenuTestBase
    {
        private IPColorField colorField;
        
        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            ActionHistorySystem.ClearHistory();
            TUtilsOverseerLoader.LoadInternalLibrary();
            
            yield return null;
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            TUtilsOverseerLoader.LoadThemeOverseer();
            
            yield return null;
            colorField = CreateAndInitColorField(Color.white);
            yield return null;
        }

        [Test]
        public void WhenValueChanged_Should_UpdateColor()
        {
            Color newColor = Color.red;
            
            colorField.GetComponentInChildren<ColorField>().UpdateValue(newColor);
            
            Assert.That(colorField.PropertyValue, Is.EqualTo(newColor));
        }

        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            FillColorField(Color.red);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Undo_Should_RevertToPreviousColor()
        {
            FillColorField(Color.red);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            
            ActionHistorySystem.Undo();
            yield return null;
            
            Assert.That(colorField.PropertyValue, Is.EqualTo(EditorDefaults.Instance.DefaultColor));
        }
        
        private void FillColorField(Color newColor)
        {
            colorField.GetComponentInChildren<ColorField>().OnPointerDown(new PointerEventData(EventSystem.current));
            ColorPickerWindow colorPickerWindow = FindFirstColorPickerWindow();
            colorPickerWindow.UpdateColor(newColor);
        }
    }
}