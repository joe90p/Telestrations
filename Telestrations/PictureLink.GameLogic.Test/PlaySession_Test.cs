using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PictureLink.Data;

namespace PictureLink.GameLogic.Test
{
    [TestClass]
    public class PlaySession_Test
    {
        [TestMethod]
        public void SetGuessType_Standard_ExpectedGuessTypeFromExistingGuess()
        {
            /*var mock = new Mock<IGuessDTO>();
            var guessType = GuessType.Drawn;
            mock.Setup(x => x.GetNextGuessType()).Returns(guessType);
            var playSession = new PlaySession() {PreviousGuess = mock.Object};
            playSession.SetGuessType();
            Assert.IsTrue(playSession.GuessType == guessType);*/
        }

        [TestMethod]
        public void SetGuessType_Standard_WrittenGuessTypeFromNullExistingGuess()
        {
            var playSession = new PlaySession();
            playSession.SetGuessType();
            Assert.IsTrue(playSession.GuessType == GuessType.Written);
        }
    }
}
