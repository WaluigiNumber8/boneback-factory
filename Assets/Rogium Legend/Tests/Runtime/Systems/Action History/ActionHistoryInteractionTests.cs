using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Systems.ActionHistory.ActionHistoryUtils;

namespace Rogium.Tests.Systems.ActionHistory
{
    /// <summary>
    /// Tests interactions with the <see cref="ActionHistorySystem"/>.
    /// </summary>
    [RequiresPlayMode]
    public class ActionHistoryInteractionTests : MenuTestWithInputBase
    {
        private readonly MenuPreparator menuPreparator = AssetDatabase.LoadAssetAtPath<MenuPreparator>("Assets/Rogium Legend/Prefabs/Game Sectors/Editor/pref_MenuPreparator.prefab");
        
        [UnityTest]
        public IEnumerator AddAndExecute_Should_GroupActions_WhenLClickHeld()
        {
            Object.Instantiate(menuPreparator.gameObject, Vector3.zero, Quaternion.identity);
            object construct = new();
            IAction action1 = CreateAction(construct);
            IAction action2 = CreateAction(construct);
            yield return null;
            
            i.Press(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action1);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action2);
            i.Release(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator AddAndExecute_Should_NotGroupActions_WhenLClickLetGo()
        {
            Object.Instantiate(menuPreparator.gameObject, Vector3.zero, Quaternion.identity);
            object construct = new();
            IAction action1 = CreateAction(construct);
            IAction action2 = CreateAction(construct);
            yield return null;
            
            i.Press(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action1);
            yield return new WaitForSecondsRealtime(0.1f);
            i.Release(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action2);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
    }
}