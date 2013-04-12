using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class Game_Test
    {
        [TestMethod]
        [ExpectedException(typeof(PlayerAlreadyExistsException))]
        public void AddPlayer_PlayerAlreadyExists_ThrowsException()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            game.PlayerPendingActions = new Dictionary<IPlayer, Action<IGuess>> {{player.Object, null}};
            game.AddPlayer(player.Object);
        }

        [TestMethod]
        public void AddPlayer_PlayerDoesNotAlreadyExists_PlayerAdded()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            game.AddPlayer(player.Object);
            Assert.IsTrue(game.PlayerPendingActions.Keys.Contains(player.Object));
        }

        [TestMethod]
        public void GetPlaySession_LongestChainForPlayerIsNull_GetNewGameSession()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            var chains = new Mock<IChainList>();
            chains.Setup(cl => cl.GetLongestChainForPlayer(It.IsAny<IPlayer>())).Returns<IChain>(null);
            game.Chains = chains.Object;
            game.AddPlayer(player.Object);
            var playSession = game.GetPlaySession(player.Object);
            Assert.IsTrue(playSession.Type==PlayType.NewGame);
        }

        [TestMethod]
        public void GetPlaySession_LongestChainForPlayerIsNotNull_GetNewGameSession()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            var chains = new Mock<IChainList>();
            var chain = new Mock<IChain>();
            chains.Setup(cl => cl.GetLongestChainForPlayer(It.IsAny<IPlayer>())).Returns(chain.Object);
            game.Chains = chains.Object;
            game.AddPlayer(player.Object);
            var playSession = game.GetPlaySession(player.Object);
            Assert.IsTrue(playSession.Type == PlayType.Link);

        }
    }
}
