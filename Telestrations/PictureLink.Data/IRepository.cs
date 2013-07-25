using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.Data
{
    public interface IRepository
    {
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> where) where T : class;

        void Delete<T>(T target);
        void Save<T>(T target);
        void Insert<T>(object target);
        IEnumerable<IChainDTO> GetUnMarkedChains();
        IChainDTO GetChain(int index);
    }
}
