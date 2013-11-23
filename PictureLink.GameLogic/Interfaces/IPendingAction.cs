using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IPendingAction
    {
        void Execute(IGuessInfo guess);
        void Release(IPlayer player);
    }
}
