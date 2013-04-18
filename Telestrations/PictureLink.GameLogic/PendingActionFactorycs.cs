using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class PendingActionFactory : IPendingActionFactory
    {
        internal IChainList ChainList { get; private set; }

        internal ILoadableDictionary<IPlayer, IPendingAction> PlayerPendingActions { get; private set; }

        public PendingActionFactory(IChainList chainList,
                                        ILoadableDictionary<IPlayer, IPendingAction> pendingPlayerActions)
        {
            this.ChainList = chainList;
            this.PlayerPendingActions = pendingPlayerActions;
        }

        public IPendingAction GetPendingAction(
            IPlayer player,
            IChain chain)
        {
            if (chain == null)
            {
                return new NewChainPendingAction(this.ChainList, this.PlayerPendingActions);
            }
            else
            {
                return new ChainAppendPendingAction(this.PlayerPendingActions, 
                                                        player, 
                                                        chain);
            }
        }
    }
}
