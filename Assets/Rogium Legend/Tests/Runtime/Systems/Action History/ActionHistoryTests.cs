using NSubstitute;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using static Rogium.Tests.Systems.ActionHistory.TUtilsActionHistory;

namespace Rogium.Tests.Systems.ActionHistory
{
    /// <summary>
    /// Tests the <see cref="ActionHistorySystem"/> class.
    /// </summary>
    public class ActionHistoryTests
    {
        private IAction mockAction;
        
        [SetUp]
        public void Setup()
        {
            mockAction = CreateAction();
            
            ActionHistorySystem.ClearHistory();
            ActionHistorySystem.ForceBeginGrouping();
        }
        
        [TearDown]
        public void TearDown() => ActionHistorySystem.ForceEndGrouping();

        [Test]
        public void AddAndExecute_Should_AddNewActionToUndo()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [Test]
        public void AddAndExecute_Should_NotAddNewActionToUndo_IfNothingChanged()
        {
            mockAction = CreateNonChangingAction();

            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(0));
        }

        [Test]
        public void AddAndExecute_Should_ExecuteAction()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            
            mockAction.Received().Execute();
        }

        [Test]
        public void AddAndExecute_Should_ClearRedoHistory()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.RedoCount, Is.EqualTo(0));
        }

        [Test]
        public void AddAndExecute_Should_AddActionToGroup_IfGroupingAllowed()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Not.Null);
            Assert.That(ActionHistorySystem.CurrentGroup.ActionsCount, Is.EqualTo(1));
        }

        [Test]
        public void AddAndExecute_Should_NotAddActionToGroup_IfGroupingBlocked()
        {
            ActionHistorySystem.ForceBeginGrouping();
            ActionHistorySystem.AddAndExecute(mockAction, true);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
        }

        [Test]
        public void AddAndExecute_Should_AddActionToGroup_IfActionIsOnSameConstruct()
        {
            object construct = new();
            mockAction = CreateAction(construct);
            IAction mockAction2 = CreateAction(construct);
            
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.AddAndExecute(mockAction2);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Not.Null);
            Assert.That(ActionHistorySystem.CurrentGroup.ActionsCount, Is.EqualTo(2));
        }

        [Test]
        public void AddAndExecute_Should_EndGroup_IfActionIsOnDifferentConstruct()
        {
            object construct1 = new();
            object construct2 = new();
            mockAction = CreateAction(construct1);
            IAction mockAction2 = CreateAction(construct2);
            
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.AddAndExecute(mockAction2);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }

        [Test]
        public void UndoLast_Should_UndoLastAction()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            
            mockAction.Received().Undo();
        }

        [Test]
        public void UndoLast_Should_ExecuteActionUndo()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            
            mockAction.Received().Undo();
        }
        
        [Test]
        public void UndoLast_Should_AddActionToRedoHistory()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            
            Assert.That(ActionHistorySystem.RedoCount, Is.EqualTo(1));
        }

        [Test]
        public void UndoLast_Should_EndGroupingAndUndoGroup_IfItWasOpened()
        {
            object construct = new();
            mockAction = CreateAction(construct);
            IAction mockAction2 = CreateAction(construct);
            
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.AddAndExecute(mockAction2);
            ActionHistorySystem.Undo();
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            mockAction.Received().Undo();
            mockAction2.Received().Undo();
        }

        [Test]
        public void RedoLast_Should_RedoLastAction()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            ActionHistorySystem.Redo();
            
            mockAction.Received().Execute();
        }

        [Test]
        public void RedoLast_Should_AddActionToUndoHistory()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            ActionHistorySystem.Redo();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
    }
}