using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Unity;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;

namespace Fdp.UI
{
    internal class BootStrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IRegionNavigationContentLoader,
                ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
            IDependencyResolver resolver = new UnityDependencyResolver(Container, App.FdpActorSystem);
            Container.RegisterInstance<IActorRefFactory>(App.FdpActorSystem,
                new ContainerControlledLifetimeManager());
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }

        protected override void ConfigureModuleCatalog()
        {
            Type moduleControls = typeof(Controls.Module);
            ModuleCatalog.AddModule(
                new ModuleInfo
                {
                    ModuleName = Strings.ControlsModule,
                    ModuleType = moduleControls.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.WhenAvailable
                });

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
                    InitializationMode = InitializationMode.WhenAvailable
                });
        }

        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
            App.Current.MainWindow.Show();
        }
    }
}