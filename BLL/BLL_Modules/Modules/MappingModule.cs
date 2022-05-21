using Autofac;
using AutoMapper;

using BLL_Mappers;

namespace BLL_Modules {
    public class MappingModule : Module {

        protected override void Load(ContainerBuilder builder) {
            
            builder.Register(context => new MapperConfiguration(cfg => {
                cfg.AddProfile<StorageMapper>();
                cfg.AddProfile<FolderMapper>();
                cfg.AddProfile<FileMapper>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c => {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

        }

    }
}