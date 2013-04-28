using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Player : IPlayer
    {
        public string Id { get; private set; }

        public Player()
        {
        }

        public Player(string id)
        {
            this.Id = id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IPlayer)
            {
                return ((IPlayer)obj).Id == this.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
