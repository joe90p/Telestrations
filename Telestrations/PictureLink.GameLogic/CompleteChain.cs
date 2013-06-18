using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public class CompleteChain : ICompleteChain
    {
        public IRepository Repository
        {
            get;
            internal set;
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasMarksAssigned()
        {
            return this.Repository.Query<IMarkDTO>(x => x.Guess.Chain.Id == this.Id).Any();
        }
    }
}
