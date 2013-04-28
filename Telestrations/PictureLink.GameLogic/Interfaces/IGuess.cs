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
        string Content { get; set; }
        bool IsPlayerContributor(IPlayer otherPlayer);
    }
}
