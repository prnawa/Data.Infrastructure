using System.Data.Entity;
using Data.Infrastructure.Core;

namespace Data.Infrastructure.EF
{
    public class UnitOfWorkFactoryEF : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWorkEF(new InfrastructureContextEF());
        }
    }
}
