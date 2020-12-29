using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcpChallenge.Utils
{
    public interface IRepository<T>
    {
        public IQueryable<T> Get();
        void Add(T entity);
        void SaveChanges();
    }
}
