using FitMax.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, new();  //unitofworks çağrıldğında repositorye erişmek için ayrı arı yönetmemek için
        void Commit(); //savechanges için
        Task CommitAsync(); //savechanges için
    }
}
