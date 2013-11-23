using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public class PlaySession : IPlaySession
    {
        public PlayType Type
        {
            get;
            private set;
        }

        public IGuessDTO PreviousGuess
        {
            get;
            internal set;
        }

        public GuessType GuessType
        {
            get;
            private set;
        }

        public PlaySession()
        {
        }

        public PlaySession(IGuessDTO previousGuess, PlayType type)
        {
            this.PreviousGuess = previousGuess;
            this.Type = type;
            this.SetGuessType();
        }

        public void SetGuessType()
        {
            this.GuessType = this.PreviousGuess == null ? GuessType.Written : Guess.GetOtherGuessType(this.PreviousGuess.Type);
        }
    }
}
