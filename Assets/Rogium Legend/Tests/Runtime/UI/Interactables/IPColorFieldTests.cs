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
using static Rogium.Tests.UI.Interactables.InteractablesCreator;
using static Rogium.Tests.UI.Interactables.InteractableUtils;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the <see cref="ColorField"/> interactable property.
    /// </summary>
    [RequiresPlayMode]
    public class IPColorFieldTests
    {
        private InteractablePropertyColorField colorField;
        
        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            ActionHistorySystem.ClearHistory();
            OverseerLoader.LoadInternalLibrary();
            yield return null;
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadThemeOverseer();
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
            
            ActionHistorySystem.UndoLast();
            yield return null;
            
            Assert.That(colorField.PropertyValue, Is.EqualTo(EditorConstants.DefaultColor));
        }
        
        private void FillColorField(Color newColor)
        {
            colorField.GetComponentInChildren<ColorField>().OnPointerDown(new PointerEventData(EventSystem.current));
            ColorPickerWindow colorPickerWindow = FindFirstColorPickerWindow();
            colorPickerWindow.UpdateValue(newColor);
        }
    }
}