using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PictureLink.GameLogic;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class Guess_Test
    {
        [TestMethod]
        public void IsPlayerGuessContributor_PlayerIsContributor_ReturnsTrue()
        {
            var contributor = new Mock<IPlayer>();
            contributor.Setup(p => p.Id).Returns("A");
            var guess = new Guess(contributor.Object);
            Assert.IsTrue(guess.IsPlayerContributor(contributor.Object));
        }

        [TestMethod]
        public void IsPlayerGuessContributor_PlayerIsNotContributor_ReturnsFalse()
        {
            var contributor = new Mock<IPlayer>();
            var otherPlayer = new Mock<IPlayer>();
            contributor.Setup(p => p.Id).Returns("A");
            otherPlayer.Setup(p => p.Id).Returns("B");
            var guess = new Guess(contributor.Object);
            Assert.IsFalse(guess.IsPlayerContributor(otherPlayer.Object));
        }
    }
}
