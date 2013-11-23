using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IChainList
    {
        IInPlayChain GetLongestChainForPlayer(IPlayer player);
        void CreateNew(IGuessInfo guess);
    }
}
