using System.Collections;
using NUnit.Framework;
using Rogium.Systems.Shortcuts;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
        private InputProfileShortcuts shortcuts;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            shortcuts = input.Shortcuts;
            affectedObject = new GameObject("AffectedObject");
            profile = CreateShortcutProfile(shortcuts.FillTool.Action, () => affectedObject.SetActive(false));
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_Trigger_WhenShortcutPressed()
        {
            i.Trigger(shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenTriggerDisabled()
        {
            profile.gameObject.SetActive(false);
            i.Trigger(shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenOverrideAllProfileIsActive()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_TriggerOnlyOverrideAllProfile()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_TriggerOnlyLatestOverrideAllProfile()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            CreateShortcutProfile(shortcuts.SelectionTool.Action, () => affectedObject3.SetActive(false), true);
            yield return null;
            i.Trigger(shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject3.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenOverrideAllProfileIsNotLatest()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            CreateShortcutProfile(shortcuts.SelectionTool.Action, () => affectedObject3.SetActive(false), true);
            yield return null;
            i.Trigger(shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_TriggerOverrideAllProfile_WhenNewerOverrideAllProfileDisabled()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            ShortcutProfile profile3 = CreateShortcutProfile(shortcuts.SelectionTool.Action, () => affectedObject3.SetActive(false), true);
            yield return null;
            profile3.gameObject.SetActive(false);
            yield return null;
            i.Trigger(shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_NotTriggerNewestOverrideAllProfile_WhenNewerOverrideAllProfileDisabled()
        {
            GameObject affectedObject2 = new("AffectedObject2");
            CreateShortcutProfile(shortcuts.EraserTool.Action, () => affectedObject2.SetActive(false), true);
            GameObject affectedObject3 = new("AffectedObject3");
            ShortcutProfile profile3 = CreateShortcutProfile(shortcuts.SelectionTool.Action, () => affectedObject3.SetActive(false), true);
            yield return null;
            profile3.gameObject.SetActive(false);
            yield return null;
            i.Trigger(shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(affectedObject3.activeSelf, Is.True);
        }
        
        private static ShortcutProfile CreateShortcutProfile(InputAction shortcut, UnityAction action, bool overrideAll = false)
        {
            ShortcutProfile p =  new GameObject("Profile").AddComponent<ShortcutProfile>();
            UnityEvent e = new();
            e.AddListener(action);
            p.Set(new []{ new ShortcutData(shortcut, e) });
            p.SetAsOverrideAll(overrideAll);
            return p;
        }
        
    }
}