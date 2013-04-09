using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class ChainList_Test
    {
        [TestMethod]
        public void GetLongestChainForPlayer_ReturnsChainWithHighestCount()
        {
            const int highestLength = 3;
            Func<int, IChain> getChain = i => { var mock = new Mock<IChain>(); 
                mock.Setup(c => c.Count).Returns(i);
                mock.Setup(c => c.IsAvailableForPlayer(It.IsAny<IPlayer>())).Returns(true);
                return mock.Object;};
            var chains = new[] { 1, highestLength, 2 }.Select(getChain);
            var chainList = new ChainList();
            chainList.AddRange(chains);
            var player  = new Mock<IPlayer>();
            var longChain = chainList.GetLongestChainForPlayer(player.Object);
            Assert.IsTrue(longChain.Count == highestLength);

        }
    }
}
