namespace PictureLink.GameLogic
{
    public interface IPlaySession
    {
        PlayType Type { get; }
        IGuess PreviousGuess { get; }
        GuessType GuessType { get; }
        void SetGuessType();
    }
}
