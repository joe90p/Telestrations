using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Game : IGame
    {
        private ILoadableDictionary<IPlayer, IPendingAction> playerPendingActions = new LoadableDictionary<IPlayer, IPendingAction>();

        internal ILoadableDictionary<IPlayer, IPendingAction> PlayerPendingActions 
        {
            get
            {
                return this.playerPendingActions;
            }
            set
            {
                this.playerPendingActions = value;
            }
        }

        public IChainList Chains { get; internal set; }

        public Game()
        {
            this.Chains = new ChainList();
            this.PendingActionFactory = new PendingActionFactory(this.Chains, this.PlayerPendingActions);
        }

        public IPendingActionFactory PendingActionFactory
        {
            get;
            internal set;
        }

        public void AddPlayer(IPlayer player)
        {
            if (this.IsPlayerInGame(player))
            {
                throw new PlayerAlreadyExistsException();
            }
            this.PlayerPendingActions.Add(player, null);
        }

        public bool IsPlayerInGame(IPlayer player)
        {
            return this.PlayerPendingActions.Keys.Contains(player);
        }

        public IPlaySession GetPlaySession(IPlayer player)
        {
            var longChain = this.Chains.GetLongestChainForPlayer(player);
            var pendingAction = this.PendingActionFactory.GetPendingAction(player, longChain);
            PlayerPendingActions.Load(player, pendingAction);
            if(longChain == null)
            {
                return new PlaySession(null, PlayType.NewGame);
            }
            else
            {
                var previousGuess = longChain.Head;
                return new PlaySession(previousGuess, PlayType.Link);
            }
        }

        public void AddGuess(IGuessInfo guess)
        {
            PlayerPendingActions[guess.Contributor].Execute(guess);
        }

        public void RemovePlayer(IPlayer player)
        {
            Maybe.From(PlayerPendingActions[player]).
                Do(p => p.Release(player));
        }
    }
}
