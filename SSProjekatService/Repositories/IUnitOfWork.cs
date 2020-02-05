using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSProjekatService.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
