using System;
using System.Collections;
using System.Threading;
using System.Web;

namespace Data.Infrastructure.Core
{
    public static class UnitOfWork
    {
        private const string HttpContextKey = "Data.Infrastructure.Core.HttpContext.Key";
        private static readonly Hashtable Threads = new Hashtable();
        public static IUnitOfWorkFactory UnitOfWorkFactory;

        public static IUnitOfWork Current
        {
            get
            {
                IUnitOfWork unitOfWork = GetCurrentUnitOfWork();

                if (unitOfWork == null)
                {
                    unitOfWork = UnitOfWorkFactory.Create();
                    SaveUnitOfWork(unitOfWork);
                }

                return unitOfWork;
            }
        }

        

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HttpContextKey] = unitOfWork;
            }
            else
            {
                lock(Threads.SyncRoot)
                {
                    Threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }

        private static IUnitOfWork GetCurrentUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HttpContextKey))
                {
                    return (IUnitOfWork)HttpContext.Current.Items[HttpContextKey];
                }
                return null;
            }
            else
            {
                Thread currentThread = Thread.CurrentThread;

                if (string.IsNullOrEmpty(currentThread.Name))
                {
                    currentThread.Name = Guid.NewGuid().ToString();
                    return null;
                }

                lock (Threads.SyncRoot)
                {
                    return (IUnitOfWork)Threads[currentThread.Name];
                }
            }
        }

        public static void SetUnitOfWorkFactory<T>() where T : IUnitOfWorkFactory
        {
            if (UnitOfWorkFactory == null)
                UnitOfWorkFactory = Activator.CreateInstance<T>();
        }
    }
}
