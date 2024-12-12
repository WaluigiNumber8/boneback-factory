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
            GameObject affectedObject2 = new GameObject("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(affectedObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_TriggerOnlyOverrideAllProfile()
        {
            GameObject affectedObject2 = new GameObject("AffectedObject2");
            CreateShortcutProfile(ShortcutType.EraserTool, () => affectedObject2.SetActive(false), true);
            yield return null;
            i.Trigger(input.Shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(affectedObject2.activeSelf, Is.False);
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