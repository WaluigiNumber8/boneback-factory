using NSubstitute;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;

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
            mockAction = Substitute.For<IAction>();
            mockAction.LastValue.Returns(0);
            mockAction.Value.Returns(1);
            mockAction.NothingChanged().Returns(false);
            
            ActionHistorySystem.ClearHistory();
            ActionHistorySystem.ForceBeginGrouping();
        }
        
        [TearDown]
        public void TearDown() => ActionHistorySystem.ForceEndGrouping();

        [Test]
        public void AddAndExecute_Should_AddNewActionToUndo()
        {
            mockAction.LastValue.Returns(0);
            mockAction.Value.Returns(1);
            mockAction.NothingChanged().Returns(false);

            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [Test]
        public void AddAndExecute_Should_NotAddNewActionToUndo_IfNothingChanged()
        {
            mockAction.LastValue.Returns(1);
            mockAction.Value.Returns(1);
            mockAction.NothingChanged().Returns(true);

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
            IAction mockAction2 = Substitute.For<IAction>();
            mockAction.AffectedConstruct.Returns(construct);
            mockAction2.AffectedConstruct.Returns(construct);
            
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.AddAndExecute(mockAction);
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Not.Null);
            Assert.That(ActionHistorySystem.CurrentGroup.ActionsCount, Is.EqualTo(2));
        }

        [Test]
        public void AddAndExecute_Should_EndGroup_IfActionIsOnDifferentConstruct()
        {
            object construct1 = new();
            object construct2 = new();
            IAction mockAction2 = Substitute.For<IAction>();
            mockAction.AffectedConstruct.Returns(construct1);
            mockAction2.AffectedConstruct.Returns(construct2);
            
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
            ActionHistorySystem.UndoLast();
            
            mockAction.Received().Undo();
        }

        [Test]
        public void UndoLast_Should_ExecuteActionUndo()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            
            mockAction.Received().Undo();
        }
        
        [Test]
        public void UndoLast_Should_AddActionToRedoHistory()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            
            Assert.That(ActionHistorySystem.RedoCount, Is.EqualTo(1));
        }

        [Test]
        public void UndoLast_Should_EndGroupingAndUndoGroup_IfItWasOpened()
        {
            object construct = new();
            IAction mockAction2 = Substitute.For<IAction>();
            mockAction.AffectedConstruct.Returns(construct);
            mockAction2.AffectedConstruct.Returns(construct);
            
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.AddAndExecute(mockAction2);
            ActionHistorySystem.UndoLast();
            
            Assert.That(ActionHistorySystem.CurrentGroup, Is.Null);
            mockAction.Received().Undo();
            mockAction2.Received().Undo();
        }

        [Test]
        public void RedoLast_Should_RedoLastAction()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            ActionHistorySystem.RedoLast();
            
            mockAction.Received().Execute();
        }

        [Test]
        public void RedoLast_Should_AddActionToUndoHistory()
        {
            ActionHistorySystem.AddAndExecute(mockAction);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            ActionHistorySystem.RedoLast();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
    }
}