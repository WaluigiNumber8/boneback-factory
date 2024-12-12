using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core;
using Rogium.Core.Shortcuts;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the <see cref="ShortcutProfile"/>.
    /// </summary>
    public class ShortcutProfileTests : MenuTestWithInputBase
    {
        private ShortcutProfile profile;
        private GameObject child;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            GameObject o = new("Profile");
            profile = o.AddComponent<ShortcutProfile>();
            child = new("Child");
            child.transform.SetParent(o.transform);
            UnityEvent e = new();
            e.AddListener(() => child.SetActive(false));
            profile.Set(new []{ new ShortcutProfile.ShortcutData(ShortcutType.FillTool, e) });
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_Trigger_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(child.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenTriggerDisabled()
        {
            profile.gameObject.SetActive(false);
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(child.activeSelf, Is.True);
        }
        
    }
}