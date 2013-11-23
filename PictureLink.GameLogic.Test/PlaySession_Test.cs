using System;
using NUnit.Framework;
using Moq;
using PictureLink.Data;

namespace PictureLink.GameLogic.Test
{
    [TestFixture]
    public class PlaySession_Test
    {
        [Test]
        public void SetGuessType_Standard_ExpectedGuessTypeFromExistingGuess()
        {
            /*var mock = new Mock<IGuessDTO>();
            var guessType = GuessType.Drawn;
            mock.Setup(x => x.GetNextGuessType()).Returns(guessType);
            var playSession = new PlaySession() {PreviousGuess = mock.Object};
            playSession.SetGuessType();
            Assert.IsTrue(playSession.GuessType == guessType);*/
        }

        [Test]
        public void SetGuessType_Standard_WrittenGuessTypeFromNullExistingGuess()
        {
            var playSession = new PlaySession();
            playSession.SetGuessType();
            Assert.IsTrue(playSession.GuessType == GuessType.Written);
        }
    }
}
