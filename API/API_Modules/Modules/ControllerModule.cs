using Autofac;

using API_Controllers;
using API_Models;
using BLL_Modules;

namespace API_Modules {
    public class ControllerModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<StorageController>().As<IController<StorageModel>>().SingleInstance();
            builder.RegisterType<FolderController>().As<IController<FolderModel>>().SingleInstance();
            builder.RegisterType<FileController>().As<IController<FileModel>>().SingleInstance();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<MappingModule>();
        }
    }
}