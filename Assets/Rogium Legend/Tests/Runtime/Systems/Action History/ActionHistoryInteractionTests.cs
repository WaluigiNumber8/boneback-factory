using System.Collections;
using NSubstitute;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Rogium.Tests.Systems.ActionHistory
{
    /// <summary>
    /// Tests interactions with the <see cref="ActionHistorySystem"/>.
    /// </summary>
    [RequiresPlayMode]
    public class ActionHistoryInteractionTests : InputTestFixture
    {
        private readonly MenuPreparator menuPreparator = AssetDatabase.LoadAssetAtPath<MenuPreparator>("Assets/Rogium Legend/Prefabs/Game Sectors/Editor/pref_MenuPreparator.prefab");
        
        private Mouse mouse;
        
        
        public override void Setup()
        {
            SceneLoader.LoadUIScene();
            base.Setup();
            mouse = InputSystem.AddDevice<Mouse>();
            Press(mouse.leftButton);
            Release(mouse.leftButton);
        }

        [UnityTest]
        public IEnumerator AddAndExecute_Should_GroupActions_WhenLClickHeld()
        {
            Object.Instantiate(menuPreparator.gameObject, Vector3.zero, Quaternion.identity);
            
            object construct = new();
            IAction action1 = ActionHistoryCreator.CreateAction(construct);
            IAction action2 = ActionHistoryCreator.CreateAction(construct);
            ActionHistorySystem.ForceEndGrouping();
            
            yield return null;
            Press(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action1);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action2);
            Release(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator AddAndExecute_Should_NotGroupActions_WhenLClickLetGo()
        {
            Object.Instantiate(menuPreparator.gameObject, Vector3.zero, Quaternion.identity);
            
            object construct = new();
            IAction action1 = ActionHistoryCreator.CreateAction(construct);
            IAction action2 = ActionHistoryCreator.CreateAction(construct);
            ActionHistorySystem.ForceEndGrouping();
            
            yield return null;
            Press(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action1);
            yield return new WaitForSecondsRealtime(0.1f);
            Release(mouse.leftButton);
            yield return new WaitForSecondsRealtime(0.1f);
            ActionHistorySystem.AddAndExecute(action2);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
    }
}