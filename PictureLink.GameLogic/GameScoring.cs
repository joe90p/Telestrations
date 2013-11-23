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
        private IRepository repository = new Repository();

        public IEnumerable<Chain> GetUnMarkedChains(int playerId)
        {
            var chains = this.repository.GetUnMarkedChains(playerId);
            return chains;

        }

        public Chain GetChain(int id)
        {
            var chain = this.repository.GetChain(id);
            return chain;
        }

        public void AwardMarks(int awarderId, Tuple<int, int>[] guessMarks)
        {
            var player = new Player(awarderId);
            var marks =
                guessMarks.Select(
                    gm =>
                    Tuple.Create(this.repository.Query<IGuessDTO>(g => g.Id == gm.Item1).FirstOrDefault(), gm.Item2)).ToArray();
            player.AwardMarks(marks);
        }
    }

    
}
