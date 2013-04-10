using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Game
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

        public Action<Guess> GetPendingAction(IPlayer player)
        {
            if (PlayerPendingActions[player] == null)
            {
                var longChain = this.Chains.GetLongestChainForPlayer(player);
                if (longChain == null)
                {
                    return this.Chains.CreateNew;
                }
                else
                {
                    longChain.Lock(player);
                    return g => { longChain.AddGuess(g); longChain.Release(g.Contributor); };
                }
            }
            return g => { };
            

        }


    }
}
