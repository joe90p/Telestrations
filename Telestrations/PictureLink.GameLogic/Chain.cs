using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Chain : List<IGuess>, IChain
    {
        public bool IsAvailableForPlayer(IPlayer player)
        {
            return !this.Any(g => g.IsPlayerContributor(player));
        }
    }
}
