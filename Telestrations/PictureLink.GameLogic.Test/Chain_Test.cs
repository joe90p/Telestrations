using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class Chain_Test
    {
        [TestMethod]
        public void IsAvailableForPlayer_PlayerIsContributorToGuessInChain_ReturnsFalse()
        {
            IsAvailableForPlayer_PlayerContributorInGuessInChainHelper(new[] { false, true }, false);
        }

        [TestMethod]
        public void IsAvailableForPlayer_PlayerIsNotContributorToGuessInChain_ReturnsTrue()
        {
            IsAvailableForPlayer_PlayerContributorInGuessInChainHelper(new[] { false, false }, true);
        }

        private void IsAvailableForPlayer_PlayerContributorInGuessInChainHelper(bool[] boolArray, bool isTrue)
        {
            var player = new Mock<IPlayer>();
            var mocks = boolArray.Select(b =>
            {
                var m = new Mock<IGuess>();
                m.Setup(g => g.IsPlayerContributor(It.IsAny<IPlayer>())).Returns(b);
                return m.Object;
            });
            var chain = new Chain();
            chain.AddRange(mocks);
            bool result = chain.IsAvailableForPlayer(player.Object);
            Assert.IsTrue(isTrue ? result : !result);
        }
    }
}
