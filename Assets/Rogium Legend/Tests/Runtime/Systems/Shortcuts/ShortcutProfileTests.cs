using System.Collections;
using NUnit.Framework;
using Rogium.Core.Shortcuts;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the <see cref="ShortcutProfile"/>.
    /// </summary>
    public class ShortcutProfileTests : MenuTestWithInputBase
    {
        private ShortcutProfile profile;
        private GameObject affectedObject;
        
        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            affectedObject = new GameObject("AffectedObject");
            profile = CreateShortcutProfile(ShortcutType.FillTool, () => affectedObject.SetActive(false));
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_Trigger_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenTriggerDisabled()
        {
            profile.gameObject.SetActive(false);
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenOverrideAllProfileIsActive()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_TriggerOnlyOverrideAllProfile()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_TriggerOnlyLatestOverrideAllProfile()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            CreateShortcutProfile(ShortcutType.SelectionTool, () => affectedObject3.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject3.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenOverrideAllProfileIsNotLatest()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            CreateShortcutProfile(ShortcutType.SelectionTool, () => affectedObject3.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_TriggerOverrideAllProfile_WhenNewerOverrideAllProfileDisabled()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            ShortcutProfile profile3 = CreateShortcutProfile(ShortcutType.SelectionTool, () => affectedObject3.SetActive(false), true);
            yield return null;
            profile3.gameObject.SetActive(false);
            yield return null;
            i.Trigger(input.Shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_NotTriggerNewestOverrideAllProfile_WhenNewerOverrideAllProfileDisabled()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            ShortcutProfile profile3 = CreateShortcutProfile(ShortcutType.SelectionTool, () => affectedObject3.SetActive(false), true);
            yield return null;
            profile3.gameObject.SetActive(false);
            yield return null;
            i.Trigger(input.Shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject3.activeSelf, Is.True);
        }
        
        private static ShortcutProfile CreateShortcutProfile(ShortcutType shortcut, UnityAction action, bool overrideAll = false)
        {
            ShortcutProfile p =  new GameObject("Profile").AddComponent<ShortcutProfile>();
            UnityEvent e = new();
            e.AddListener(action);
            p.Set(new []{ new ShortcutProfile.ShortcutData(shortcut, e) });
            p.SetAsOverrideAll(overrideAll);
            return p;
        }
        
    }
}