namespace PictureLink.GameLogic
{
    public class Guess : IGuess
    {
        public IPlayer Contributor { get; private set; }

        public string Content { get; set; }

        public GuessType Type
        {
            get; 
            internal set; 
        }

        public Guess(IPlayer player)
        {
            this.Contributor = player;
        }

        public Guess(IPlayer player, string content, GuessType guessType)
        {
            this.Contributor = player;
            this.Content = content;
            this.Type = guessType;
        }

        public bool IsPlayerContributor(IPlayer otherPlayer)
        {
            return this.Contributor.Id == otherPlayer.Id;
        }

        public GuessType GetNextGuessType()
        {
            return GetOtherGuessType(this.Type);
        }

        public static GuessType GetOtherGuessType(GuessType guessType)
        {
            return guessType == GuessType.Drawn ? GuessType.Written : GuessType.Drawn;
        }
    }
}
