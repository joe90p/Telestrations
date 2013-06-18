using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class GuessFactory : IGuessFactory
    {
        private IChain chain;

        public GuessFactory(IChain chain)
        {
            this.chain = chain;
        }
        public IGuess MakeGuess(IGuessInfo guessInfo)
        {
            return new Guess(guessInfo, this.chain);
        }
    }
}
