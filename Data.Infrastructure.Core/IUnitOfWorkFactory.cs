namespace Data.Infrastructure.Core
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
