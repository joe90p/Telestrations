using System;
using Moq;
using NUnit.Framework;

namespace PictureLink.GameLogic.Test
{
    [TestFixture]
    public class ChainAppendPendingAction_Test
    {
        [Test]
        public void Execute_Standard_CallsNullPendingAction()
        {
            var player = new Mock<IPlayer>();
            var guess = new Mock<IGuessInfo>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();
            var chain = new Mock<IInPlayChain>();

            guess.Setup(g => g.Contributor).Returns(player.Object);
            pendingActions.Setup(pa => pa.Load(It.Is<IPlayer>(p => p == player.Object), It.Is<IPendingAction>(p => p == null))).Verifiable();

            var newchainAction = new ChainAppendPendingAction() {PlayerPendingActions = pendingActions.Object, InPlayChain = chain.Object};
            newchainAction.Execute(guess.Object);
            pendingActions.Verify();
        }
    }
}
