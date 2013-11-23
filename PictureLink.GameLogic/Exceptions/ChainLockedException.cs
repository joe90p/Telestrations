using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class ChainLockedException : Exception
    {

        public ChainLockedException(string message)
            : base(message)
        {
        }
    }
}
