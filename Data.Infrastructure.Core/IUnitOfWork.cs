using System;

namespace Data.Infrastructure.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
