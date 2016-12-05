using Fdp.InfraStructure;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;

namespace Fdp.UI
{
    class BootStrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            Type moduleEssentials = typeof(Essentials.Module);
            ModuleCatalog.AddModule(
                new ModuleInfo
                {
                    ModuleName = Strings.EssentialsModule,
                    ModuleType = moduleEssentials.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.WhenAvailable
                });
            Type moduleDataModeller = typeof(DataModeller.Module);
            ModuleCatalog.AddModule(
                new ModuleInfo
                {
                    ModuleName = Strings.DataModellerModule,
                    ModuleType = moduleDataModeller.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.OnDemand
                });


        }

    }
}
