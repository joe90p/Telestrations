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
            game.PlayerPendingActions = new LoadableDictionary<IPlayer, Action<IGuess>> {{player.Object, null}};
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
            game.PendingActionFactory = new Mock<IPendingActionFactory>().Object;
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
            game.PendingActionFactory = new Mock<IPendingActionFactory>().Object;
            game.AddPlayer(player.Object);
            var playSession = game.GetPlaySession(player.Object);
            Assert.IsTrue(playSession.Type == PlayType.Link);

        }

        [TestMethod]
        public void GetPlaySession_Standard_SetsPendingPlayerAction()
        {
            var player = new Mock<IPlayer>().Object;
            var game = new Game();
            var chains = new Mock<IChainList>();
            var chain = new Mock<IChain>();
            var actionFactory = new Mock<IPendingActionFactory>();
            var playerPendingActions = new Mock<ILoadableDictionary<IPlayer, Action<IGuess>>>();
            Action<IGuess> action = g => { };
            actionFactory.Setup(af => af.GetPendingAction(It.IsAny<IPlayer>(),
                                                            It.IsAny<IChain>())).Returns(action);
            playerPendingActions.Setup(o => o.Load(It.Is<IPlayer>(p => p == player), It.Is<Action<IGuess>>(ag => ag == action))).Verifiable();

            game.Chains = chains.Object;
            game.PendingActionFactory = actionFactory.Object;
            game.PlayerPendingActions = playerPendingActions.Object;
            game.GetPlaySession(player);


            playerPendingActions.Verify();
        }
    }
}
