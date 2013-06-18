namespace PictureLink.GameLogic
{
    public class Guess : IGuess
    {
        public IPlayer Contributor { get; private set; }

        public string Content { get; set; }

        public IChain ParentChain {  get; set; }

        public GuessType Type
        {
            get; 
            internal set; 
        }

        public Guess(IPlayer player)
        {
            this.Contributor = player;
        }

        public Guess(IGuessInfo guessInfo, IChain parentChain)
        {
            this.Contributor = guessInfo.Contributor;
            this.Content = guessInfo.Content;
            this.Type = guessInfo.Type;
            this.ParentChain = parentChain;
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
