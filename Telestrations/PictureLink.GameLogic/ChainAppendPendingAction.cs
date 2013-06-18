using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    class ChainAppendPendingAction : IPendingAction
    {
        internal ILoadableDictionary<IPlayer, IPendingAction> PlayerPendingActions
        {
            get; 
            set; 
        }

        internal IInPlayChain InPlayChain { get; set; }

        public void Execute(IGuessInfo guess)
        {
            this.InPlayChain.AddGuess(guess);
            this.Release(guess.Contributor);
        }

        public void Release(IPlayer player)
        {
            this.InPlayChain.Release(player);
            this.PlayerPendingActions.Load(player, null);
        }


        public  ChainAppendPendingAction(ILoadableDictionary<IPlayer, IPendingAction> playerPendingActions,
            IPlayer player,
            IInPlayChain inPlayChain)
        {
            this.InPlayChain = inPlayChain;
            this.PlayerPendingActions = playerPendingActions;
            this.InPlayChain.Lock(player);
        }

        internal ChainAppendPendingAction() {}
    }
}
