using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public interface IPlaySession
    {
        PlayType Type { get; }
        IGuessDTO PreviousGuess { get; }
        GuessType GuessType { get; }
        void SetGuessType();
    }
}
