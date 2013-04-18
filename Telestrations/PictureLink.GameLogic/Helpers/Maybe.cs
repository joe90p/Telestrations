using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public static class Maybe
    {
        public static Maybe<T> From<T>(T value) where T : class
        { return new Maybe<T>(value); }
    }



    public class Maybe<T> where T : class
    {
        private readonly T value;

        public T Value
        {
            get
            {
                return value;
            }
        }

        public Maybe(T value)
        {
            this.value = value;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> getter) where TResult : class
        {
            return new Maybe<TResult>((value == null) ? null : getter(value));
        }

        public void Do(Action<T> doIt)
        {
            if(value == null)
            {
                doIt(value);
            }
        }
    }
}
