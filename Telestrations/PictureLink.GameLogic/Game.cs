using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Game : IGame
    {
        private IDictionary<IPlayer, Action<IGuess>> playerPendingActions = new Dictionary<IPlayer, Action<IGuess>>();
        public IDictionary<IPlayer, Action<IGuess>> PlayerPendingActions 
        { 
            get
            {
                if(this.playerPendingActions == null)
                {
                    this.playerPendingActions = new Dictionary<IPlayer, Action<IGuess>>();
                }
                return this.playerPendingActions;
            }
            internal set { this.playerPendingActions = value; }
        }
        public IChainList Chains { get; internal set; }

        public void AddPlayer(IPlayer player)
        {
            if (this.PlayerPendingActions.Keys.Contains(player))
            {
                throw new PlayerAlreadyExistsException();
            }
            this.PlayerPendingActions.Add(player, null);
        }

        public Action<IGuess> GetPendingAction(IPlayer player, IChain chain)
        {
            if (chain == null)
            {
                return this.Chains.CreateNew;
            }
            else
            {
                chain.Lock(player);
                return g => { chain.AddGuess(g); chain.Release(g.Contributor); };
            }
        }

        public IPlaySession GetPlaySession(IPlayer player)
        {
            var longChain = this.Chains.GetLongestChainForPlayer(player);
            var pendingAction = this.GetPendingAction(player, longChain);
            PlayerPendingActions[player] = pendingAction;
            return longChain == null ? new PlaySession(null, PlayType.NewGame)
                : new PlaySession(longChain.Head, PlayType.Link);
        }
    }
}
