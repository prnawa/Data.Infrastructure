using Domain.Core.DomainEntities;

namespace Domain.Core.RepositoryContracts
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}
