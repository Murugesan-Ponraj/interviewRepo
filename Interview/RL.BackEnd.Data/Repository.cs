using RL.Data;
using Microsoft.EntityFrameworkCore;
using RL.Data.DataModels;
using RL.Data.DataModels.Common;

namespace RL.BackEnd.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RLContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(RLContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
