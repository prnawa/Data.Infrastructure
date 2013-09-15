using System.Data.Entity;
using Data.Infrastructure.Core;
using Data.Infrastructure.EF;
using NUnit.Framework;

namespace Data.Infrastructure.Tests
{
    [SetUpFixture]
    public class InfrastructureSetUp
    {
        [SetUp]
        public void Setup()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<InfrastructureContextEF>());
            UnitOfWork.SetUnitOfWorkFactory<UnitOfWorkFactoryEF>();
        }

        [TearDown]
        public void TearDown()
        {
            ClearDatabase<InfrastructureContextEF>();
        }

        public void ClearDatabase<T>() where T : DbContext, new()
        {
            using (var context = new T())
            {
                var tableNames = context.Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'");
                foreach (var tableName in tableNames)
                {
                    context.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE {0}", tableName));
                }
                context.SaveChanges();
            }
        }
    }
}