using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class ChainList : List<IInPlayChain>, IChainList
    {
        public IInPlayChain GetLongestChainForPlayer(IPlayer player)
        {
            return this.
                Where(c => c.IsAvailableForPlayer(player)).
                OrderBy(c => c.Count).
                LastOrDefault();
        }

        public void CreateNew(IGuessInfo guess)
        {
            var chain = new InPlayChain(guess);
            this.Add(chain);
        }

    }

    

    

    
}
