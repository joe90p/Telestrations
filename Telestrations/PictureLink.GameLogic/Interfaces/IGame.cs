using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IGame
    {
        IChainList Chains { get;}
        IPendingActionFactory PendingActionFactory{ get;}
        void AddPlayer(IPlayer player);
        IPlaySession GetPlaySession(IPlayer player);
        void AddGuess(IGuess guess);
        void RemovePlayer(IPlayer player);
        bool IsPlayerInGame(IPlayer player);
    }
}
