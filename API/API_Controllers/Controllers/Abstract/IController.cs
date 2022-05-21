using System.Collections.Generic;

namespace API_Controllers {
    public interface IController<TModel> where TModel : class {

        void Add(TModel model);
        void Update(TModel model);
        void Remove(TModel model);
       
        TModel GetById(int id);
        void Remove(int id);
        
        List<TModel> GetAll();

    }
}