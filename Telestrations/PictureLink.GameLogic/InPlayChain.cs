using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;
using PictureLink.Data.Test;

namespace PictureLink.GameLogic
{
    public class InPlayChain : IInPlayChain
    {
        public const int MaximumLength = 2;

        private const string LockedMessage = "This chain is locked to {0}";

        internal IGuessFactory GuessFactory { get; set; }

        public IList<IGuessDTO> Guesses
        {
            get;
            internal set;
        }
        public event EventHandler<EventArgs> MaximumChainLengthReached;

        public IPlayer LockedBy
        {
            get;
            internal set;
        }

        public int Count
        {
            get { return this.Guesses.Count; }
        }

        public IGuessDTO Head
        {
            get { return this.Guesses.Last(); }
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

        public InPlayChain()
        {
            this.GuessFactory = new GuessFactory(this);
            this.Guesses = new List<IGuessDTO>();
        }

        public InPlayChain(IGuessInfo guessInfo)
        {
            this.GuessFactory = new GuessFactory(this);
            this.Guesses = new List<IGuessDTO> { this.GuessFactory.MakeGuess(guessInfo) };
        }

        public bool IsAvailableForPlayer(IPlayer player)
        {
            return LockedBy==null && !HasPlayerContributedGuess(player);
        }

        public bool HasPlayerContributedGuess(IPlayer player)
        {
            return this.Guesses.Any(g => g.Contributor.Id == player.Id);
        }

        public void AddGuess(IGuessInfo guessInfo)
        {
            if (HasPlayerContributedGuess(guessInfo.Contributor))
            {
                string message = String.Format(
                    "{0} has already contributed a guess to the chain", Maybe.From(guessInfo.Contributor).
                                                                                            Select(c => c.Name).Value);
                throw new ChainLockedException(message);
            }
            if (guessInfo.Contributor.Id != this.LockedBy.Id)
            {
                throw new ChainLockedException(GetLockedMessage());
            }
            this.Guesses.Add(this.GuessFactory.MakeGuess(guessInfo));
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
            return String.Format(LockedMessage, Maybe.From(this.LockedBy).Select(c => c.Name).Value);
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
