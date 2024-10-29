using System.Collections;
using Rogium.Editors.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Editors;
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
            ExternalLibraryOverseer.Instance.ClearCampaigns();
            ActionHistorySystem.ClearHistory();
            AssetCreator.AddNewCampaignToLibrary();
            yield return null;
        }
    }
}