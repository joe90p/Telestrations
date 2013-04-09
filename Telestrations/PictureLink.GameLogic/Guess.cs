using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Guess : IGuess
    {
        public IPlayer Contributor { get; private set; }

        public Guess(IPlayer player)
        {
            this.Contributor = player;
        }

        public bool IsPlayerContributor(IPlayer otherPlayer)
        {
            return this.Contributor.Id == otherPlayer.Id;
        }
    }
}
