using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class Chain : IChain
    {
        public const int MaximumLength = 8;

        private const string LockedMessage = "This chain is locked to player with id {0}";

        private IGuessFactory guessFactory;

        internal List<IGuess> Guesses
        {
            get;
            set;
        }
        public event EventHandler<EventArgs> MaximumChainLengthReached;

        public IPlayer LockedBy
        {
            get;
            private set;
        }

        public int Count
        {
            get { return this.Guesses.Count; }
        }

        public IGuess Head
        {
            get { return this.Guesses.Last(); }
        }

        public Chain()
        {
            this.guessFactory = new GuessFactory(this);
            this.Guesses = new List<IGuess>();
        }

        public Chain(IGuessInfo guessInfo)
        {
            this.guessFactory = new GuessFactory(this);
            this.Guesses = new List<IGuess>{this.guessFactory.MakeGuess(guessInfo)};
        }

        public bool IsAvailableForPlayer(IPlayer player)
        {
            return LockedBy==null && !HasPlayerContributedGuess(player);
        }

        public bool HasPlayerContributedGuess(IPlayer player)
        {
            return this.Guesses.Any(g => g.IsPlayerContributor(player));
        }

        public void AddGuess(IGuessInfo guessInfo)
        {
            if (HasPlayerContributedGuess(guessInfo.Contributor))
            {
                string message = String.Format(
                    "Player with id {0} has already contributed a guess to the chain", Maybe.From(guessInfo.Contributor).
                                                                                            Select(c => c.Id));
                throw new ChainLockedException(message);
            }
            if (guessInfo.Contributor.Id != this.LockedBy.Id)
            {
                throw new ChainLockedException(GetLockedMessage());
            }
            this.Guesses.Add(this.guessFactory.MakeGuess(guessInfo));
            if (Guesses.Count == MaximumLength)
            {
                this.OnMaximumChainLengthReached();
            }
        }

        public void Lock(IPlayer player)
        {
            if(this.LockedBy != null)
            {
                throw new ChainLockedException(this.GetLockedMessage());    
            }
            this.LockedBy = player;
        }

        private string GetLockedMessage()
        {
            return String.Format(LockedMessage, Maybe.From(this.LockedBy).Select(c => c.Id));
        }     

        private void OnMaximumChainLengthReached()
        {
            var handler = MaximumChainLengthReached;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Release(IPlayer player)
        {
            if (this.LockedBy.Id != player.Id)
            {
                throw new ChainLockedException(this.GetLockedMessage());
            }
            this.LockedBy = null;
        }
    }


}
