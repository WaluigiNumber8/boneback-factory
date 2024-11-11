using System.Collections;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.TestTools;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the Input Binding interactable property.
    /// </summary>
    public class IPInputBindingTests : MenuTestBase
    {
        private InteractablePropertyInputBinding inputBinding;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            inputBinding = InteractablesCreator.CreateAndInitInputBinding();
        }

        [Test]
        public void Should_SetTitle_WhenConstructed()
        {
            inputBinding.Construct("Test Title");
            Assert.That(inputBinding.Title, Is.EqualTo("Test Title"));
        }
    }
}