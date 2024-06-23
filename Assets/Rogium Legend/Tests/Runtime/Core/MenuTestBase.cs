using System.Collections;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the Menu test scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class MenuTestBase
    {
        [UnitySetUp]
        public virtual IEnumerator Setup()
        {
            SceneLoader.LoadMenuTestingScene();
            yield return null;
        }
    }
}