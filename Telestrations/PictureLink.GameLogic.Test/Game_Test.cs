using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

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
            game.Players = new List<IPlayer>() { player.Object };
            game.AddPlayer(player.Object);
        }
    }
}
