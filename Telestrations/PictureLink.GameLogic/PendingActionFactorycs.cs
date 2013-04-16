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

        internal ILoadableDictionary<IPlayer, Action<IGuess>> PlayerPendingActions { get; private set; }

        public PendingActionFactory(IChainList chainList,
                                        ILoadableDictionary<IPlayer, Action<IGuess>> pendingPlayerActions)
        {
            this.ChainList = chainList;
            this.PlayerPendingActions = pendingPlayerActions;
        }
        
        public Action<IGuess> GetPendingAction(
            IPlayer player,
            IChain chain)
        {
            if (chain == null)
            {
                return g => 
                { 
                    this.ChainList.CreateNew(g);
                    this.PlayerPendingActions.Load(g.Contributor, null);
                };
            }
            else
            {
                chain.Lock(player);
                return g => 
                { 
                    chain.AddGuess(g); 
                    chain.Release(g.Contributor);
                    this.PlayerPendingActions.Load(g.Contributor, null);
                };
            }
        }
    }
}
