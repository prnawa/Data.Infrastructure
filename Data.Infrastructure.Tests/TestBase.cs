using System;
using System.Linq;
using System.Reflection;
using Data.Infrastructure.EF;
using Data.Infrastructure.EF.Repositories;
using Domain.Core.DomainEntities;

namespace Data.Infrastructure.Tests
{
    public class TestBase
    {
        public ITestContext<T> TestContext<T>() where T : class
        {
            var instance = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof (ITestContext<T>)) && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(k => Activator.CreateInstance(k) as ITestContext<T>).FirstOrDefault();

            return instance;
        }
    }

    public interface ITestContext<T> where T : class
    {
        T CreateDefault();
        void Save(T entity);
    }

    public class UserContext : ITestContext<User>
    {
        public User CreateDefault()
        {
            const string email = "default@gmail.com";

            var user = new User
            {
                FirstName = "Ruwan",
                LastName = "Nawarathne",
                Email = email,
                Created = DateTime.Now
            };

            Save(user);

            return user;
        }

        public void Save(User entity)
        {
            using (var context = new InfrastructureContextEF())
            {
                context.Users.Add(entity);
                context.SaveChanges();
            }
        }
    }
}
