using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IPlaySession
    {
        public PlayType Type { get; }
        public IGuess Guess { get; }
    }
}
