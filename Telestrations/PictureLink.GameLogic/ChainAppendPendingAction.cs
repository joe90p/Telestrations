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

        internal IChain Chain { get; set; }

        public void Execute(IGuess guess)
        {
            this.Chain.AddGuess(guess);
            this.Release(guess.Contributor);
        }

        public void Release(IPlayer player)
        {
            this.Chain.Release(player);
            this.PlayerPendingActions.Load(player, null);
        }


        public  ChainAppendPendingAction(ILoadableDictionary<IPlayer, IPendingAction> playerPendingActions,
            IPlayer player,
            IChain chain)
        {
            this.Chain = chain;
            this.PlayerPendingActions = playerPendingActions;
            this.Chain.Lock(player);
        }

        internal ChainAppendPendingAction() {}
    }
}
