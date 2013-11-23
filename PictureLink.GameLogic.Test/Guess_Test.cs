using System;
using NUnit.Framework;
using Moq;
using PictureLink.Data;
using PictureLink.GameLogic;

namespace PictureLink.GameLogic.Test
{
    [TestFixture]
    public class Guess_Test
    {
        [Test]
        public void IsPlayerGuessContributor_PlayerIsContributor_ReturnsTrue()
        {
            /*var contributor = new Mock<IPlayer>();
            contributor.Setup(p => p.Id).Returns("A");
            var guess = new Guess(contributor.Object);
            Assert.IsTrue(guess.IsPlayerContributor(contributor.Object));*/
        }

        [Test]
        public void IsPlayerGuessContributor_PlayerIsNotContributor_ReturnsFalse()
        {
            /*var contributor = new Mock<IPlayer>();
            var otherPlayer = new Mock<IPlayer>();
            contributor.Setup(p => p.Id).Returns("A");
            otherPlayer.Setup(p => p.Id).Returns("B");
            var guess = new Guess(contributor.Object);
            Assert.IsFalse(guess.IsPlayerContributor(otherPlayer.Object));*/
        }

        [Test]
        public void GetOtherGuessType_WrittenReturnsDrawn()
        {
            var guess = Guess.GetOtherGuessType(GuessType.Written);
            Assert.IsTrue(guess == GuessType.Drawn);
        }

        [Test]
        public void GetOtherGuessType_DrawnReturnsWritten()
        {
            var guess = Guess.GetOtherGuessType(GuessType.Drawn);
            Assert.IsTrue(guess == GuessType.Written);
        }
    }
}
