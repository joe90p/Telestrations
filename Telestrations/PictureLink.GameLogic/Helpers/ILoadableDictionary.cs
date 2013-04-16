using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface ILoadableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        void Load(TKey key, TValue value);
    }
}
