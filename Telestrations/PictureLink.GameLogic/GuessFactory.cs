using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class GuessFactory : IGuessFactory
    {
        private IInPlayChain inPlayChain;

        public GuessFactory(IInPlayChain inPlayChain)
        {
            this.inPlayChain = inPlayChain;
        }
        public IGuess MakeGuess(IGuessInfo guessInfo)
        {
            return new Guess(guessInfo, this.inPlayChain);
        }
    }
}
