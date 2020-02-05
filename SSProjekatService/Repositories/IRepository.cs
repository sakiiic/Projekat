using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSProjekatService.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Add(T newEntity);
        void Remove(T entity);
        void Update(T entity);
        IUnitOfWork UnitOfWork { get; }
    }
}
