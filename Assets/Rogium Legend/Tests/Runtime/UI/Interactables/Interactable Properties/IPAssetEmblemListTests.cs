using System.Collections;
using NUnit.Framework;
using RedRats.Systems.Themes;
using Rogium.Editors.Packs;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;
using static Rogium.Tests.UI.Interactables.Properties.IPAssetEmblemListTestsU;

namespace Rogium.Tests.UI.Interactables.Properties
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
            yield return CreateEmblemListOfPalettes();
            Assert.That(Object.FindFirstObjectByType<AssetEmblemList>().EmblemCount, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes.Count));
        }

        [UnityTest]
        public IEnumerator Should_ShowProperIconsInEmblems_WhenConstructed()
        {
            yield return CreateEmblemListOfPalettes();
            Assert.That(Object.FindFirstObjectByType<AssetEmblemList>().GetEmblem(0), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_UpdateEmblemListTitleTheme_WhenConstructed()
        {
            ThemeOverseerMono.GetInstance().ChangeTheme(ThemeType.Green);
            yield return CreateEmblemListOfPalettes();
            TextMeshProUGUI title = Object.FindFirstObjectByType<InteractablePropertyAssetEmblemList>().GetComponentInChildren<TextMeshProUGUI>();
            Assert.That(title.color, Is.EqualTo(ThemeOverseerMono.GetInstance().CurrentThemeData.Fonts.general.color));
            Assert.That(title.font, Is.EqualTo(ThemeOverseerMono.GetInstance().CurrentThemeData.Fonts.general.font));
            Assert.That(title.fontSize, Is.EqualTo(ThemeOverseerMono.GetInstance().CurrentThemeData.Fonts.general.size));
        }
    }
}