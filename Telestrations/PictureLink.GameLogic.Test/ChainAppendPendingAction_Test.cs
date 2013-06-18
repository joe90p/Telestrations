using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class ChainAppendPendingAction_Test
    {
        [TestMethod]
        public void Execute_Standard_CallsNullPendingAction()
        {
            var player = new Mock<IPlayer>();
            var guess = new Mock<IGuess>();
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
