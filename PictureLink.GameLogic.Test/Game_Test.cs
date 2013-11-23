using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace PictureLink.GameLogic.Test
{
    [TestFixture]
    public class Game_Test
    {
        [Test]
        [ExpectedException(typeof(PlayerAlreadyExistsException))]
        public void AddPlayer_PlayerAlreadyExists_ThrowsException()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            game.PlayerPendingActions = new LoadableDictionary<IPlayer, IPendingAction> {{player.Object, null}};
            game.AddPlayer(player.Object);
        }

        [Test]
        public void AddPlayer_PlayerDoesNotAlreadyExists_PlayerAdded()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            game.AddPlayer(player.Object);
            Assert.IsTrue(game.PlayerPendingActions.Keys.Contains(player.Object));
        }

        [Test]
        public void GetPlaySession_LongestChainForPlayerIsNull_GetNewGameSession()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            var chains = new Mock<IChainList>();
            chains.Setup(cl => cl.GetLongestChainForPlayer(It.IsAny<IPlayer>())).Returns<IInPlayChain>(null);
            game.Chains = chains.Object;
            game.PendingActionFactory = new Mock<IPendingActionFactory>().Object;
            game.AddPlayer(player.Object);
            var playSession = game.GetPlaySession(player.Object);
            Assert.IsTrue(playSession.Type==PlayType.NewGame);
        }

        [Test]
        public void GetPlaySession_LongestChainForPlayerIsNotNull_GetNewGameSession()
        {
            var player = new Mock<IPlayer>();
            var game = new Game();
            var chains = new Mock<IChainList>();
            var chain = new Mock<IInPlayChain>();
            chains.Setup(cl => cl.GetLongestChainForPlayer(It.IsAny<IPlayer>())).Returns(chain.Object);
            game.Chains = chains.Object;
            game.PendingActionFactory = new Mock<IPendingActionFactory>().Object;
            game.AddPlayer(player.Object);
            var playSession = game.GetPlaySession(player.Object);
            Assert.IsTrue(playSession.Type == PlayType.Link);

        }

        [Test]
        public void GetPlaySession_Standard_SetsPendingPlayerAction()
        {
            var player = new Mock<IPlayer>().Object;
            var game = new Game();
            var chains = new Mock<IChainList>();
            var chain = new Mock<IInPlayChain>();
            var actionFactory = new Mock<IPendingActionFactory>();
            var playerPendingActions = new Mock<ILoadableDictionary<IPlayer, IPendingAction>>();
            var pendingAction = new Mock<IPendingAction>();
            actionFactory.Setup(af => af.GetPendingAction(It.IsAny<IPlayer>(),
                                                            It.IsAny<IInPlayChain>())).Returns(pendingAction.Object);
            playerPendingActions.Setup(o => o.Load(It.Is<IPlayer>(p => p == player), It.Is<IPendingAction>(p => p == pendingAction.Object))).Verifiable();

            game.Chains = chains.Object;
            game.PendingActionFactory = actionFactory.Object;
            game.PlayerPendingActions = playerPendingActions.Object;
            game.GetPlaySession(player);


            playerPendingActions.Verify();
        }
    }
}
