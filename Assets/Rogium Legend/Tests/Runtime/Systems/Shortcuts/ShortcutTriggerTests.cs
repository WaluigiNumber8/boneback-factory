using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core;
using Rogium.Core.Shortcuts;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the <see cref="ShortcutTrigger"/>.
    /// </summary>
    public class ShortcutTriggerTests : MenuTestWithInputBase
    {
        private ShortcutTrigger trigger;
        private Button button;
        private InteractableEventCaller caller;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            GameObject o = new("Trigger");
            button = o.AddComponent<Button>();
            caller = o.AddComponent<InteractableEventCaller>();
            trigger = o.AddComponent<ShortcutTrigger>();
            trigger.Set(ShortcutType.FillTool);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_Trigger_WhenShortcutPressed()
        {
            bool triggered = false;
            button.onClick.AddListener(() => triggered = true);
            yield return null;
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(triggered, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotTrigger_WhenTriggerDisabled()
        {
            bool triggered = false;
            button.onClick.AddListener(() => triggered = true);
            yield return null;
            trigger.gameObject.SetActive(false);
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(triggered, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_TriggerEffects_WhenTriggered()
        {
            bool triggered = false;
            caller.OnClickLeft += () => triggered = true;
            yield return null;
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(triggered, Is.True);
        }
        
    }
}