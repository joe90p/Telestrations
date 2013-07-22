using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using PictureLink.Data;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class ChainList_Test
    {
        [TestMethod]
        public void GetLongestChainForPlayer_Standard_ReturnsChainWithHighestCount()
        {
            const int highestLength = 3;
            Func<int, IInPlayChain> getChain = i => { var mock = new Mock<IInPlayChain>(); 
                mock.Setup(c => c.Count).Returns(i);
                mock.Setup(c => c.IsAvailableForPlayer(It.IsAny<IPlayer>())).Returns(true);
                return mock.Object;};
            var chains = new[] { 1, highestLength, 2 }.Select(getChain);
            var chainList = new ChainList {Chains = chains.ToList()};
            var player  = new Mock<IPlayer>();
            var longChain = chainList.GetLongestChainForPlayer(player.Object);
            Assert.IsTrue(longChain.Count == highestLength);

        }

        [TestMethod]
        public void chain_MaximumChainLengthReached_Standard_ChainIsRemoved()
        {
            var mockChain = new Mock<IInPlayChain>();
            var guesses = Enumerable.Range(1, 3).Select(i => new Mock<IGuessDTO>().Object);
            var mockRepo = new Mock<IRepository>();
            var mockChains = new Mock<IList<IInPlayChain>>();
            var chainList = new ChainList(mockChains.Object, mockRepo.Object);
            mockChain.Setup(c => c.Guesses).Returns(guesses.ToList());

            chainList.chain_MaximumChainLengthReached(mockChain.Object, null);
            
            mockChains.Verify(cs => cs.Remove(It.Is<IInPlayChain>(c => c.Equals(mockChain.Object))));

        }

        [TestMethod]
        public void chain_MaximumChainLengthReached_Standard_ChainIsSaved()
        {
            var mockChain = new Mock<IInPlayChain>();
            var guesses = Enumerable.Range(1, 3).Select(i => new Mock<IGuessDTO>().Object);
            var mockRepo = new Mock<IRepository>();
            var mockChains = new Mock<IList<IInPlayChain>>();
            var chainList = new ChainList(mockChains.Object, mockRepo.Object);
            mockChain.Setup(c => c.Guesses).Returns(guesses.ToList());

            chainList.chain_MaximumChainLengthReached(mockChain.Object, null);
            
            mockRepo.Verify(r => r.Save(It.Is<IInPlayChain>(c => c.Equals(mockChain.Object))));
        }

        [TestMethod]
        public void chain_MaximumChainLengthReached_Standard_GuessesAreSaved()
        {
            var mockChain = new Mock<IInPlayChain>();
            var guesses = Enumerable.Range(1, 3).Select(i => new Mock<IGuessDTO>().Object).ToList();
            var mockRepo = new Mock<IRepository>();
            var mockChains = new Mock<IList<IInPlayChain>>();
            var chainList = new ChainList(mockChains.Object, mockRepo.Object);
            mockChain.Setup(c => c.Guesses).Returns(guesses);

            chainList.chain_MaximumChainLengthReached(mockChain.Object, null);
            
            foreach(var g in guesses)
            {
                mockRepo.Verify(r => r.Save(It.Is<IGuessDTO>(c => c.Equals(g))));
            }
            
        }
    }
}
