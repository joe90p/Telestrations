using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public class Player : IPlayer
    {
        public int Id { get; private set; }

        public IRepository Repository
        {
            get; internal set; }

        public Player()
        {
        }

        public Player(int id)
        {
            this.Id = id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var player = obj as IPlayer;
            if (player != null)
            {
                return player.Id == this.Id;
            }
            else
            {
                return false;
            }
        }

        public void AwardMarks(Tuple<IGuessDTO, int>[] marks)
        {
            var chainId = marks[0].Item1.Chain.Id;

            if(marks.Select(m => m.Item1.Chain.Id).Any(x => x != chainId))
            {
                throw new InvalidOperationException();
            }

            var chain = this.Repository.Query<IChainDTO>(c => c.Id == chainId).FirstOrDefault();

            var chainComplete = new CompleteChain(chain);
            
            

            if (chainComplete.HasMarksAssigned(this.Id))
            {
                throw new InvalidOperationException();
            }
            foreach(var mark in marks)
            {
                this.Repository.Insert<IMarkDTO>(new MarkDTO() {Guess = mark.Item1, Score = mark.Item2, Awarder = this});
            }    
        }



        public string Name
        {
            get; private set; }
    }
}
