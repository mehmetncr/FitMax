using FitMax.DataAccess.Contexts;
using FitMax.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FitMaxContext _context;
        private DbSet<T> _dbSet;

        public Repository(FitMaxContext context)
        {
            _context = context;  //veritabanı
            _dbSet = _context.Set<T>();  //ilgili tablo
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity); //async kayıt yapma
        }
        public void AddNoAsync(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDeleted") != null) //silinmek istenen verinin isDeleted kolunu var mı diye bakar varsa değerini true yapar
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true); //değeri true yapar
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }

        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);  //nesneyi bul
            this.Delete(entity);  //silmek için gönder
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }


        // filtre(x=>x) -- sıralama -- birleştirilecek tablolar
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;  //tabloyu alır filtreleri uygulayarak filtrelenmiş verileri dödürür
            if (filter != null)  //filtre varsa
            {
                query = query.Where(filter);
            }
            if (orderby != null)  //sıralama istenmişse
            {
                query = orderby(query);
            }
            foreach (var table in includes)  //ilişkili tablolar istenmişse
            {
                query = query.Include(table);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();  //EF verileri takip etmiyor değiştirmeyecemiz için
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);


        }
    }
}
