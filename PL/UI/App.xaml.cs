using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Autofac;

using UI.Scenes;
using UI.Modules;

namespace UI {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {

            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            var container = builder.Build();

            using(var scope = container.BeginLifetimeScope()) {
                
                var mainWindow = scope.Resolve<MainWindow>();

                scope.Resolve<ContentScene>();
                scope.Resolve<StorageScene>();
                scope.Resolve<OpenStorageScene>();

                mainWindow.Show();
                base.OnStartup(e);
            }

        }

    }
}