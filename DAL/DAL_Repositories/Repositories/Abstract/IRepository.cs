using System.Collections.Generic;

namespace DAL_Repositories {
    public interface IRepository<TEntity> where TEntity : class {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        TEntity GetById(int id);
        void Remove(int id);
        
        List<TEntity> GetAll();

    }
}