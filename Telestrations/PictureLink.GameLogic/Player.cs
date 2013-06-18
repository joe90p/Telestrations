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
        public string Id { get; private set; }

        public IRepository Repository
        {
            get; internal set; }

        public Player()
        {
        }

        public Player(string id)
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

        public void AwardMarks(ICompleteChain chain, Tuple<IGuess, int>[] marks)
        {
            if(marks.Any(t => t.Item1.Chain.Id != chain.Id))
            {
                throw new InvalidOperationException();
            }
            
            if (marks.Length != 3)
            {
                throw new InvalidOperationException();
            }

            if(chain.HasMarksAssigned())
            {
                throw new InvalidOperationException();
            }
            foreach(var mark in marks)
            {
                this.Repository.Insert<IMarkDTO>(new MarkDTO() {Guess = mark.Item1, Score = mark.Item2, Awarder = this});
            }    
        }

    }
}
