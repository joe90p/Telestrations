using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IGuess
    {
        IPlayer Contributor { get; }
        bool IsPlayerContributor(IPlayer otherPlayer);
    }
}
