using System;
using System.Data.Entity;
using Data.Infrastructure.Core;

namespace Data.Infrastructure.EF
{
    internal class UnitOfWorkEF : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWorkEF(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Database.Connection.Close();
                Context.Database.Connection.Dispose();
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
