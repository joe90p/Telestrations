using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using PictureLink.Data;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class Chain_Test
    {
        [TestMethod]
        public void HasPlayerContribuedGuess_PlayerIsContributorToGuessInChain_ReturnsFalse()
        {
            HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(new[] { false, true }, true);
        }

        [TestMethod]
        public void HasPlayerContribuedGuess_PlayerIsNotContributorToGuessInChain_ReturnsTrue()
        {
            HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(new[] { false, false }, false);
        }

        [TestMethod]
        public void IsAvailableForPlayer_ChainIsLocked_ReturnsFalse()
        {
            var chain = new InPlayChain();
            chain.Lock(new Mock<IPlayer>().Object);

        }

        [TestMethod]
        public void AddGuess_MaximumLengthReached_FiresHandler()
        {
            /*var chain = new InPlayChain();
            bool handlerHit = false;
            chain.MaximumChainLengthReached += (o, e) => handlerHit = true;
            var mockGuessInfo = new Mock<IGuessInfo>();
            var player = new Mock<IPlayer>();
            mockGuessInfo.Setup(gi => gi.Contributor).Returns(player.Object);
            chain.LockedBy = player.Object;
            //var mockGuesses = new List<IGuess>();
            Func<IGuessDTO> getGuess = () => { var mockGuess = new Mock<IGuessDTO>();
                                         mockGuess.Setup(g => g.IsPlayerContributor(It.IsAny<IPlayer>())).Returns(false);
                                         return mockGuess.Object;
            };

            var guesses = Enumerable.Range(1, 7).Select(i => getGuess()).ToList();

            chain.Guesses = guesses;
            chain.AddGuess(mockGuessInfo.Object);
            Assert.IsTrue(handlerHit);*/
        }

        [TestMethod]
        public void AddGuess_ContributorLockedChain_GuessAdded()
        {
            var chain = new InPlayChain();
            var player1 = new Mock<IPlayer>();
            var guessInfo = new Mock<IGuessInfo>();
            var guessFactory = new Mock<IGuessFactory>();
            guessInfo.Setup(g => g.Contributor).Returns(player1.Object);
            chain.LockedBy = player1.Object;
            chain.GuessFactory = guessFactory.Object;
            var guess = new Mock<IGuessDTO>();
            var guesses = new List<IGuessDTO>();

            guessFactory.Setup(gf => gf.MakeGuess(It.Is<IGuessInfo>(g => g.Equals(guessInfo.Object)))).Returns(guess.Object);

            chain.Guesses = guesses;
            chain.AddGuess(guessInfo.Object);
            Assert.IsTrue(chain.Guesses.Contains(guess.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void AddGuess_ContributorDidNotLockChain_ExceptionThrown()
        {
            var chain = new InPlayChain();
            var player1 = new Mock<IPlayer>();
            var player2 = new Mock<IPlayer>();
            player1.Setup(p => p.Id).Returns(1);
            player2.Setup(p => p.Id).Returns(2);
            var guess = new Mock<IGuessInfo>();
            guess.Setup(g => g.Contributor).Returns(player2.Object);
            chain.Lock(player1.Object);
            chain.AddGuess(guess.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void AddGuess_ContributorAlreadyHasGuessInChain_ExceptionThrown()
        {
            /*var chain = new InPlayChain();

            var mockGuess = new Mock<IGuessDTO>();
            var mockPlayer = new Mock<IPlayer>();
            var mockGuessInfo = new Mock<IGuessInfo>();
            mockGuess.Setup(g => g.IsPlayerContributor(It.Is<IPlayer>(p => p.Equals(mockPlayer.Object)))).Returns(true);
            mockGuessInfo.Setup(g => g.Contributor).Returns(mockPlayer.Object);
            chain.Guesses = new List<IGuessDTO> { mockGuess.Object };
            chain.LockedBy = mockPlayer.Object;
            chain.AddGuess(mockGuessInfo.Object);*/
        }

        [TestMethod]
        public void Release_PlayerIsLocker_LockedByIsSetToNull()
        {
            var chain = new InPlayChain();
            var player = new Mock<Player>();
            chain.Lock(player.Object);
            chain.Release(player.Object);
            Assert.IsTrue(chain.LockedBy == null);
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void Release_PlayerIsNotLocker_ExceptionThrown()
        {
            var chain = new InPlayChain();
            var player1 = new Mock<IPlayer>();
            var player2 = new Mock<IPlayer>();
            player1.Setup(p => p.Id).Returns(1);
            player2.Setup(p => p.Id).Returns(2);
            chain.Lock(player1.Object);
            chain.Release(player2.Object);
        }

        [TestMethod]
        public void Lock_LockedByIsNull_ChainIsLockedByPlayer()
        {
            var chain = new InPlayChain();
            var player = new Mock<Player>();
            chain.Lock(player.Object);
            Assert.IsTrue(chain.LockedBy==player.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void Lock_LockedByIsNotNull_ExceptionThrown()
        {
            var chain = new InPlayChain();
            var player1 = new Mock<Player>();
            var player2 = new Mock<Player>();
            chain.Lock(player1.Object);
            chain.Lock(player2.Object);
        }


        private void HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(bool[] boolArray, bool isTrue)
        {
            /*var player = new Mock<IPlayer>();
            var mocks = boolArray.Select(b =>
            {
                var m = new Mock<IGuessDTO>();
                if(b)
                {
                m.Setup(gi => gi.IsPlayerContributor(It.Is<IPlayer>(p => p.Equals(player.Object)))).Returns(b);
                }
                return m.Object;
            });
            var chain = new InPlayChain();
            chain.Guesses = mocks.ToList();
            bool result = chain.HasPlayerContributedGuess(player.Object);
            Assert.IsTrue(isTrue ? result : !result);*/
        }
    }
}
