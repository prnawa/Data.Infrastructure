using System.Data.Entity;
using Domain.Core.DomainEntities;

namespace Data.Infrastructure.EF
{
    public class InfrastructureContextEF : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public InfrastructureContextEF() : base("name=InfrastructureContextEF") { }
    }
}
