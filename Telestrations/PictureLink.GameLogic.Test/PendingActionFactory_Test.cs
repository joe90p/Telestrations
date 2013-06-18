using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class PendingActionFactory_Test
    {
        [TestMethod]
        public void GetPendingAction_PassNullChain_ResultingActionCreatesNewChain()
        {
            var chainList = new Mock<IChainList>();
            var player = new Mock<IPlayer>();
            var guess = new Mock<IGuess>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();

            chainList.Setup(t => t.CreateNew(It.Is<IGuess>(g => g== guess.Object))).Verifiable();
            var factory = new PendingActionFactory(chainList.Object, pendingActions.Object);
            var action = factory.GetPendingAction(player.Object, null);
            action.Execute(guess.Object);
            chainList.Verify();
        }

        [TestMethod]
        public void GetPendingAction_PassNonNullChain_LocksChainToPlayer()
        {
            var chainList = new Mock<IChainList>();
            var player = new Mock<IPlayer>();
            var chain = new Mock<IInPlayChain>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();

            chain.Setup(c => c.Lock(It.Is<IPlayer>(p => p == player.Object))).Verifiable();
            var factory = new PendingActionFactory(chainList.Object, pendingActions.Object);
            factory.GetPendingAction(player.Object, chain.Object);
            chain.Verify();
        }

        [TestMethod]
        public void GetPendingAction_PassNonNullChain_ResultingActionCallsAddGuessAndRelease()
        {
            var chainList = new Mock<IChainList>();
            var player = new Mock<IPlayer>();
            var chain = new Mock<IInPlayChain>();
            var guess = new Mock<IGuess>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();

            guess.Setup(g => g.Contributor).Returns(player.Object);
            chain.Setup(c => c.AddGuess(It.Is<IGuess>(g => g == guess.Object))).Verifiable();
            chain.Setup(c => c.Release(It.Is<IPlayer>(p => p==player.Object))).Verifiable();

            var factory = new PendingActionFactory(chainList.Object, pendingActions.Object);
            var action = factory.GetPendingAction(player.Object, chain.Object);
            action.Execute(guess.Object);
            chain.Verify();
        }

        //TODO: change this to test the release method
        /*[TestMethod]
        public void GetPendingAction_Standard_AllResultingActionCallsNullPendingAction()
        {
            var chainList = new Mock<IChainList>();
            var player = new Mock<IPlayer>();
            var inPlayChain = new Mock<IInPlayChain>();
            var guess = new Mock<IGuess>();
            var pendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();

            guess.Setup(g => g.Contributor).Returns(player.Object);
            pendingActions.Setup(pa => pa.Load(It.Is<IPlayer>(p => p == player.Object), It.Is<Action<IGuess>>(g => g == null))).Verifiable();

            var factory = new PendingActionFactory(chainList.Object, pendingActions.Object);
            var action = factory.GetPendingAction(player.Object, inPlayChain.Object);
            action(guess.Object);
            pendingActions.Verify();
        }*/
    }
}
