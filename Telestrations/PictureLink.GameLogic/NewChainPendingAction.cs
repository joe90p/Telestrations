using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class NewChainPendingAction : IPendingAction
    {
        internal ILoadableDictionary<IPlayer, IPendingAction> PlayerPendingActions { get; set; }
        internal IChainList ChainList { get; set; }


        public void Execute(IGuess guess)
        {
            this.ChainList.CreateNew(guess);
            this.Release(guess.Contributor);
        }

        public void Release(IPlayer player)
        {
            this.PlayerPendingActions.Load(player, null);
        }

        public NewChainPendingAction(IChainList chainList,
            ILoadableDictionary<IPlayer, IPendingAction> playerPendingActions)
        {
            this.PlayerPendingActions = playerPendingActions;
            this.ChainList = chainList;
        }

        internal NewChainPendingAction()
        {

        }
    }
}
