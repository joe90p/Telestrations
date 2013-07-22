using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;
using PictureLink.Data.Test;

namespace PictureLink.GameLogic
{
    public class GameScoring
    {
        private IRepository repository = new MockHelper.MockRepository();

        public IEnumerable<IChainDTO> GetUnMarkedChains(int playerId)
        {
            var chains = this.repository.GetUnMarkedChains();
            return chains;

        }
    }

    
}
