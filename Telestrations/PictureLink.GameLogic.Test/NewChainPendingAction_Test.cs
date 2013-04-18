using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class NewChainPendingAction_Test
    {
        [TestMethod]
        public void Execute_Standard_CallsNullPendingAction()
        {
            var player = new Mock<IPlayer>();
            var guess = new Mock<IGuess>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();
            var chainList = new Mock<IChainList>();

            guess.Setup(g => g.Contributor).Returns(player.Object);
            pendingActions.Setup(pa => pa.Load(It.Is<IPlayer>(p => p == player.Object), It.Is<IPendingAction>(p => p == null))).Verifiable();

            var newchainAction = new NewChainPendingAction() { PlayerPendingActions = pendingActions.Object, ChainList = chainList.Object};
            newchainAction.Execute(guess.Object);
            pendingActions.Verify();
        }

    }
}
