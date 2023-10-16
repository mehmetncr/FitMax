using FitMax.DataAccess.Contexts;
using FitMax.DataAccess.Repositories;
using FitMax.Entity.Interfaces;
using FitMax.Entity.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitMaxContext _context;
        private bool disposed = false;

        public UnitOfWork(FitMaxContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context); //burdan context gelirse di dan istemeyecek
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)  //dispose edilmediyse manuel et
            {
                if (disposing)  //default false verildi dispse edilmemişse true olacak
                {
                    _context.Dispose();
                }

            }

            this.disposed = true; // dispose edildi işareti
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  //
        }
    }
}
