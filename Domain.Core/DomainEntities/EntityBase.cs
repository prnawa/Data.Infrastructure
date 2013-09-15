using System;

namespace Domain.Core.DomainEntities
{
    public abstract class EntityBase<TUniqueId> 
    {
        public TUniqueId Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
