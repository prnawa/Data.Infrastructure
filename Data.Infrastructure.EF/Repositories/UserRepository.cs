using System.Linq;
using Domain.Core.DomainEntities;
using Domain.Core.RepositoryContracts;

namespace Data.Infrastructure.EF.Repositories
{
    public class UserRepository : GenericRepositoryEF<User, int>, IUserRepository
    {
        public User GetUserByEmail(string email)
        {
            return GetAll().SingleOrDefault(u => u.Email.Equals(email));
        }
    }
}
