using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.Data
{
    public class MarkDTO : IMarkDTO
    {
        public IGuessDTO Guess { get;
            set;
        }

        public int Id { get;
            set;
        }


        public IPlayerDTO Awarder
        {
            get; set; }

        public IPlayerDTO Awardee
        {
            get;
            set;
        }
        public int Score
        {
            get; set; }
    }
}
