using System.Data.Entity;

using DAL_Database;
using DAL_Entities;

using System.Collections.Generic;
using System.Linq;

namespace DAL_Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        
        public readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity) {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity) {
            _context.SaveChanges();
        }

        public void Remove(TEntity entity) {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public TEntity GetById(int id) {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public void Remove(int id) {
            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }

        public List<TEntity> GetAll() {
            if(typeof(TEntity) == typeof(Storage)) { return _dbSet.ToList(); }
            else { return _dbSet.ToList(); }
        }

    }
}