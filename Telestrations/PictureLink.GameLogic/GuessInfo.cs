using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class GuessInfo : IGuessInfo
    {
        public string Content
        {
            get; 
            private set;
        }

        public IPlayer Contributor
        {
            get; 
            private set;
        }

        public GuessType Type
        {
            get; 
            private set;
        }

        public GuessInfo(IPlayer player, string content, GuessType guessType)
        {
            this.Contributor = player;
            this.Content = content;
            this.Type = guessType;
        }
    }
}
