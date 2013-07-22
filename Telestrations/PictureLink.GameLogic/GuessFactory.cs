using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public class GuessFactory : IGuessFactory
    {
        private IChainDTO chain;

        public GuessFactory(IChainDTO chain)
        {
            this.chain = chain;
        }
        public IGuessDTO MakeGuess(IGuessInfo guessInfo)
        {
            return new Guess(guessInfo, this.chain);
        }
    }
}
