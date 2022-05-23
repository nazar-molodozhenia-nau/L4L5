using API_Modules;

using Autofac;

using UI.Scenes;

namespace UI.Modules {
    public class MainModule : Module {
        protected override void Load(ContainerBuilder builder) {
            
            builder.RegisterModule<ControllerModule>();

            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<ContentScene>().SingleInstance();
            builder.RegisterType<StorageScene>().SingleInstance();
            builder.RegisterType<OpenStorageScene>().SingleInstance();

        }
    }
}