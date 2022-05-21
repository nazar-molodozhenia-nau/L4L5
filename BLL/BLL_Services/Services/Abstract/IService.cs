using System.Collections.Generic;

namespace BLL_Services {
    public interface IService<TDomain> where TDomain : class {
        
        void Add(TDomain domain);
        void Update(TDomain domain);
        void Remove(TDomain domain);
        
        TDomain GetById(int id);
        void Remove(int id);

        List<TDomain> GetAll();
    }
}