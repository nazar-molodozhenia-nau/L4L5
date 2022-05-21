using DAL_Entities;
using DAL_Repositories;

namespace DAL_UofW {
    public interface IUnitOfWork {

        IRepository<Storage> Storage { get; }
        IRepository<Folder> Folder { get; }
        IRepository<File> File { get; }

        void Save();

    }
}