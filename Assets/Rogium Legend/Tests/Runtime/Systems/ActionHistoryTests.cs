using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Input;
using UnityEngine.TestTools;

namespace Rogium.Tests.Systems.ActionHistory
{
    /// <summary>
    /// Tests the <see cref="ActionHistorySystem"/> class.
    /// </summary>
    public class ActionHistoryTests
    {

        [SetUp]
        public void Setup()
        {
            ActionHistorySystem.RedoLast(); //Init the system
        }
        
        [Test]
        public void Add_NewAction_ToUndo()
        {
            IAction action = Substitute.For<IAction>();
            action.Value.Returns(1);
            action.LastValue.Returns(0);
            action.NothingChanged().Returns(false);

            ActionHistorySystem.AddAndExecute(action);
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
    }
}