using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Game : IGame
    {
        public Dictionary<IPlayer, Action<Guess>> PlayerPendingActions { get; internal set; }
        public ChainList Chains { get; private set; }

        public void AddPlayer(IPlayer player)
        {
            if (this.PlayerPendingActions.Keys.Contains(player))
            {
                throw new PlayerAlreadyExistsException();
            }
            this.PlayerPendingActions.Add(player, null);
        }

        public Action<Guess> GetPendingAction(IPlayer player, IChain chain)
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
            if (PlayerPendingActions[player] == null)
            {
                var longChain = this.Chains.GetLongestChainForPlayer(player);
                var pendingAction = this.GetPendingAction(player, longChain);
                PlayerPendingActions[player] = pendingAction;
                return longChain == null ? new PlaySession(null, PlayType.NewGame) 
                    : new PlaySession(longChain.Head, PlayType.Link);
            }
            else
            {
                throw new Exception();
            }
        }


    }
}
