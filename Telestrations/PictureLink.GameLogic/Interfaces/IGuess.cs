using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public interface IGuess : IGuessDTO
    {
        IPlayer Contributor { get; }
        string Content { get; set; }
        bool IsPlayerContributor(IPlayer otherPlayer);
        GuessType Type { get; }
        GuessType GetNextGuessType();
    }
}
