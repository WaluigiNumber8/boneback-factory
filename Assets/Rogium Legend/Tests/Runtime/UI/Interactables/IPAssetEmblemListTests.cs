using System.Collections;
using System.Linq;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the Asset Emblem List Interactable property.
    /// </summary>
    public class IPAssetEmblemListTests : MenuTestBase
    {
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return CreateAndAssignPack();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadInternalLibrary();
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ShowProperEmblemCount_WhenConstructed()
        {
            UIPropertyBuilder.GetInstance().BuildAssetEmblemList("Test", PackEditorOverseer.Instance.CurrentPack.Palettes.Select(pal => pal.Icon).ToList());
            yield return null;
            Assert.That(Object.FindFirstObjectByType<AssetEmblemList>().EmblemCount, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes.Count));
        }

        [UnityTest]
        public IEnumerator Should_ShowProperIconsInEmblems_WhenConstructed()
        {
            UIPropertyBuilder.GetInstance().BuildAssetEmblemList("Test", PackEditorOverseer.Instance.CurrentPack.Palettes.Select(pal => pal.Icon).ToList());
            yield return null;
            Assert.That(Object.FindFirstObjectByType<AssetEmblemList>().GetEmblem(0), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon));
        }
    }
}