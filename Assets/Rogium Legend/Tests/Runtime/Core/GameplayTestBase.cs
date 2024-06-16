using System.Collections;
using UnityEngine.TestTools;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// A base for all play mode, that run on the Gameplay test scene.
    /// </summary>
    [RequiresPlayMode]
    public abstract class GameplayTestBase
    {
        [UnitySetUp]
        public virtual IEnumerator Setup()
        {
            SceneLoader.LoadGameplayTestingScene();
            yield return null;
        }
    }
}