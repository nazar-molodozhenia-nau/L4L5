using Autofac;

using DAL_Database;
using DAL_Entities;
using DAL_Repositories;
using DAL_UofW;

namespace DAL_Modules {
    public class UnitofWorkModule : Module {
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<ApplicationDbContext>().SingleInstance();

            builder.RegisterType<Repository<Storage>>().As<IRepository<Storage>>().SingleInstance();
            builder.RegisterType<Repository<Folder>>().As<IRepository<Folder>>().SingleInstance();
            builder.RegisterType<Repository<File>>().As<IRepository<File>>().SingleInstance();
        }
    }
}