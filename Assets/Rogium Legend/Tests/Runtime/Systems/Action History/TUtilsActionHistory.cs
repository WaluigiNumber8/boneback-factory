using NSubstitute;
using Rogium.Systems.ActionHistory;

namespace Rogium.Tests.Systems.ActionHistory
{
    public static class TUtilsActionHistory
    {
        public static IAction CreateAction(object construct = null)
        {
            IAction mockAction = Substitute.For<IAction>();
            mockAction.LastValue.Returns(0);
            mockAction.Value.Returns(1);
            mockAction.NothingChanged().Returns(false);
            mockAction.AffectedConstruct.Returns(construct);
            return mockAction;
        }
        
        public static IAction CreateNonChangingAction(object construct = null)
        {
            IAction mockAction = Substitute.For<IAction>();
            mockAction.LastValue.Returns(1);
            mockAction.Value.Returns(1);
            mockAction.NothingChanged().Returns(false);
            mockAction.AffectedConstruct.Returns(construct);
            return mockAction;
        }
    }
}