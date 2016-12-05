using Fdp.Essentials;
using Fdp.InfraStructure;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Globalization;
using System.Reflection;
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

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            //{
            //    var viewName = viewType.FullName;
            //    viewName = viewName.Replace(".Views.", ".ViewModels.");
            //    var viewAsseblyName = viewType.GetTypeInfo().Assembly.FullName;
            //    var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            //    var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}{1}", viewName, suffix);
            //    var assembly = viewType.GetTypeInfo().Assembly;
            //    var type = assembly.GetType(viewModelName, true);

            //    return type;
            //});
        }
    }
}
