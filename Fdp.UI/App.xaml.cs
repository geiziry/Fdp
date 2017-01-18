using Akka.Actor;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Fdp.UI
{
    public partial class App : Application
    {
        private static ActorSystem _fdpActorSystem;
        public static ActorSystem FdpActorSystem => _fdpActorSystem;

        protected override void OnStartup(StartupEventArgs e)
        {
            _fdpActorSystem = ActorSystem.Create("FdpActorSystem");
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016ColorfulName;

            base.OnStartup(e);
            var bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _fdpActorSystem.Shutdown();
            _fdpActorSystem.AwaitTermination();
            base.OnExit(e);
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).StackTrace);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.GetBaseException().ToString());
            e.Handled = true;
        }
    }
}