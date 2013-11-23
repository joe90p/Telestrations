using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public class LoadableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ILoadableDictionary<TKey, TValue>
    {
        public void Load(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                this.Add(key, value);
            }
        }
    }
}
