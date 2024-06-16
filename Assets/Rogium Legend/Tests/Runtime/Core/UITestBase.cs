using System.Collections;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the UI scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class UITestBase
    {
        [UnitySetUp]
        public virtual IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            yield return null;
        }
    }
}