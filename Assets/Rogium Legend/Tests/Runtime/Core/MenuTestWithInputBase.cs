using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the UI scene.
    /// </summary>
    public abstract class MenuTestWithInputBase : MenuTestBase
    {
        protected readonly InputTestFixture i = new();
        
        protected Keyboard keyboard;
        protected Mouse mouse;
        protected Gamepad gamepad;
        
        [UnitySetUp]
        public virtual IEnumerator SetUp()
        {
            i.Setup();
            mouse = InputSystem.AddDevice<Mouse>();
            keyboard = InputSystem.AddDevice<Keyboard>();
            gamepad = InputSystem.AddDevice<Gamepad>();
            i.Press(mouse.leftButton);
            i.Release(mouse.leftButton);
            i.Press(keyboard.spaceKey);
            i.Release(keyboard.spaceKey);
            i.Press(gamepad.buttonSouth);
            i.Release(gamepad.buttonSouth);

            yield return base.Setup();
        }
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return null;
            InputSystem.RemoveDevice(mouse);
            InputSystem.RemoveDevice(keyboard);
            InputSystem.RemoveDevice(gamepad);
            i.TearDown();
        }
    }
}