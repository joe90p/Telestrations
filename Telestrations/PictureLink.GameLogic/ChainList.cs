using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;
using PictureLink.Data.Test;

namespace PictureLink.GameLogic
{
    public class ChainList : IChainList
    {
        public IList<IInPlayChain> Chains { get; set; }
        public IRepository Repository { get; set; }

        public ChainList(IList<IInPlayChain> chains, IRepository repository)
        {
            this.Chains = chains;
            this.Repository = repository;
        }

        public ChainList()
        {
            this.Chains = new List<IInPlayChain>();
            this.Repository = new MockHelper.MockRepository();
        }

        public IInPlayChain GetLongestChainForPlayer(IPlayer player)
        {
            return this.Chains.
                Where(c => c.IsAvailableForPlayer(player)).
                OrderBy(c => c.Count).
                LastOrDefault();
        }

        public void CreateNew(IGuessInfo guess)
        {
            var chain = new InPlayChain(guess);
            chain.MaximumChainLengthReached += chain_MaximumChainLengthReached;
            this.Chains.Add(chain);
        }

        public void chain_MaximumChainLengthReached(object sender, EventArgs e)
        {
            var s = sender as IInPlayChain;
            this.Chains.Remove(s);
            this.Repository.Save(s);
            foreach(var guess in s.Guesses)
            {
                this.Repository.Save(guess);
            }
        }

    }

    

    

    
}
