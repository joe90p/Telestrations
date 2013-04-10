using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class Chain_Test
    {
        [TestMethod]
        public void HasPlayerContribuedGuess_PlayerIsContributorToGuessInChain_ReturnsFalse()
        {
            HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(new[] { false, true }, false);
        }

        [TestMethod]
        public void HasPlayerContribuedGuess_PlayerIsNotContributorToGuessInChain_ReturnsTrue()
        {
            HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(new[] { false, false }, true);
        }

        [TestMethod]
        public void IsAvailableForPlayer_ChainIsLocked_ReturnsFalse()
        {
            var chain = new Chain();
            chain.Lock(new Mock<IPlayer>().Object);

        }

        [TestMethod]
        public void AddGuess_MaximumLengthReached_FiresHandler()
        {
            var chain = new Chain();
            bool handlerHit = false;
            chain.MaximumChainLengthReached += (o, e) => handlerHit = true;
            Func<IGuess> getGuess = () =>
            {
                var mock = new Mock<IGuess>();
                mock.Setup(g => g.IsPlayerContributor(It.IsAny<IPlayer>()))
                 .Returns(false);
                return mock.Object;
            };
            var guesses = Enumerable.Range(1, 7).
                Select(i => getGuess());
            chain.Guesses = guesses.ToList();
            chain.AddGuess(getGuess());
            Assert.IsTrue(handlerHit);
        }

        [TestMethod]
        public void AddGuess_ContributorLockedChain_GuessAdded()
        {
            var chain = new Chain();
            var player1 = new Mock<IPlayer>();
            var guess = new Mock<IGuess>();
            guess.Setup(g => g.Contributor).Returns(player1.Object);
            chain.Lock(player1.Object);
            chain.AddGuess(guess.Object);
            Assert.IsTrue(chain.Head==guess.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void AddGuess_ContributorDidNotLockChain_ExceptionThrown()
        {
            var chain = new Chain();
            var player1 = new Mock<IPlayer>();
            var player2 = new Mock<IPlayer>();
            var guess = new Mock<IGuess>();
            guess.Setup(g => g.Contributor).Returns(player2.Object);
            chain.Lock(player1.Object);
            chain.AddGuess(guess.Object);
            Assert.IsTrue(chain.Head == guess.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void AddGuess_ContributorAlreadyHasGuessInChain_ExceptionThrown()
        {
            var chain = new Chain();

            Func<IGuess> getGuess = () =>
                                        {
                                            var guess = new Mock<IGuess>();
                                            guess.Setup(g => g.IsPlayerContributor(It.IsAny<IPlayer>())).Returns(true);
                                            return guess.Object;
                                        };

            var guesses = Enumerable.Range(1, 1).
                Select(i => getGuess()).
                ToList();

            chain.Guesses = guesses;
            chain.AddGuess(getGuess());
        }

        [TestMethod]
        public void Release_PlayerIsLocker_LockedByIsSetToNull()
        {
            var chain = new Chain();
            var player = new Mock<Player>();
            chain.Lock(player.Object);
            chain.Release(player.Object);
            Assert.IsTrue(chain.LockedBy == null);
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void Release_PlayerIsNotLocker_ExceptionThrown()
        {
            var chain = new Chain();
            var player1 = new Mock<Player>();
            var player2 = new Mock<Player>();
            chain.Lock(player1.Object);
            chain.Release(player2.Object);
        }

        [TestMethod]
        public void Lock_LockedByIsNull_ChainIsLockedByPlayer()
        {
            var chain = new Chain();
            var player = new Mock<Player>();
            chain.Lock(player.Object);
            Assert.IsTrue(chain.LockedBy==player.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ChainLockedException))]
        public void Lock_LockedByIsNotNull_ExceptionThrown()
        {
            var chain = new Chain();
            var player1 = new Mock<Player>();
            var player2 = new Mock<Player>();
            chain.Lock(player1.Object);
            chain.Lock(player2.Object);
        }


        private void HasPlayerContribuedGuess_PlayerContributorInGuessInChainHelper(bool[] boolArray, bool isTrue)
        {
            var player = new Mock<IPlayer>();
            var mocks = boolArray.Select(b =>
            {
                var m = new Mock<IGuess>();
                m.Setup(g => g.IsPlayerContributor(It.IsAny<IPlayer>())).Returns(b);
                return m.Object;
            });
            var chain = new Chain();
            mocks.Select(
                m =>
                    {
                        chain.AddGuess(m);
                        return 0;
                    }).ToList();
            bool result = chain.HasPlayerContribuedGuess(player.Object);
            Assert.IsTrue(isTrue ? result : !result);
        }
    }
}
