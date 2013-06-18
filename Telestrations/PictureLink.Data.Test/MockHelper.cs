using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace PictureLink.Data.Test
{
    public class MockHelper
    {
        public IRepository GetMockRepository()
        {
            return new MockRepository();
        }

        public class MockRepository : IRepository
        {
            public IEnumerable<T> Query<T>(Expression<Func<T, bool>> where) where T : class
            {
                return Enumerable.Range(1, 9).Select(x => new Mock<T>().Object);
            }

            public void Delete<T>(T target)
            {

            }
            
            public void Save<T>(T target)
            {

            }
            public void Insert<T>(object target)
            {

            }

        }
    }
}
