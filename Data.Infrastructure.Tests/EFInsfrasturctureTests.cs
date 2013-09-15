using System;
using Data.Infrastructure.Core;
using Data.Infrastructure.EF.Repositories;
using Domain.Core.DomainEntities;
using NUnit.Framework;

namespace Data.Infrastructure.Tests
{
    [TestFixture]
    public class EFInsfrasturctureTests : TestBase
    {
        [Test]
        public void GetUserByEmail_ExistingUserWithValidEmail_ReturnsExpectedResult()
        {
            var user = TestContext<User>().CreateDefault();

            try
            {
                var userRepository = new UserRepository();
                var loadedUser = userRepository.GetUserByEmail(user.Email);
                UnitOfWork.Current.Commit();
                Assert.AreEqual(user, loadedUser);
            }
            finally
            {
                UnitOfWork.Current.Dispose();
            }
        }
    }
}
