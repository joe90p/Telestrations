using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public interface IInPlayChain : IChainDTO
    {
        bool IsAvailableForPlayer(IPlayer player);
        void AddGuess(IGuessInfo guess);
        int Count { get; }
        IGuessDTO Head { get; }
        void Lock(IPlayer player);
        IPlayer LockedBy { get; }
        void Release(IPlayer player);
        IList<IGuessDTO> Guesses { get; }
    }
}
