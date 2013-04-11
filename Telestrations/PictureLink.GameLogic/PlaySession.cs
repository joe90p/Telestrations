using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class PlaySession : IPlaySession
    {
        public PlayType Type
        {
            get;
            private set;
        }

        public IGuess Guess
        {
            get;
            private set;
        }

        public PlaySession(IGuess guess, PlayType type)
        {
            this.Guess = guess;
            this.Type = type;
        }
    }
}
