using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the UI scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class UITestWithInputBase : InputTestFixture
    {
        protected Mouse mouse;

        public override void Setup()
        {
            base.Setup();
            mouse = InputSystem.AddDevice<Mouse>();
            Press(mouse.leftButton);
            Release(mouse.leftButton);
        }

        public override void TearDown()
        {
            InputSystem.RemoveDevice(mouse);
            base.TearDown();
        }
        
        [UnitySetUp]
        public virtual IEnumerator SetUp()
        {
            SceneLoader.LoadUIScene();
            yield return null;
        }
    }
}