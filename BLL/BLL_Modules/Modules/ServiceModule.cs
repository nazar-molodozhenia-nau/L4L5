using Autofac;

using BLL_Domains;
using BLL_Services;
using DAL_Modules;

namespace BLL_Modules {
    public class ServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<StorageService>().As<IService<StorageDomain>>().SingleInstance();
            builder.RegisterType<FileService>().As<IService<FolderDomain>>().SingleInstance();
            builder.RegisterType<FileService>().As<IService<FileDomain>>().SingleInstance();
            builder.RegisterModule<UnitofWorkModule>();
        }
    }
}