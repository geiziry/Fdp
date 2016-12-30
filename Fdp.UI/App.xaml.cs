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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationThemeHelper.ApplicationThemeName =
                Theme.Office2016ColorfulName;
            base.OnStartup(e);
            var bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = string.Empty;
            string AssemblyName = new AssemblyName(args.Name).Name;
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //if (AssemblyName.StartsWith("Microsoft"))
            assemblyPath = Path.Combine(folderPath , AssemblyName + ".dll");//+ "\\Microsoft.Practices"
            //else
            //    assemblyPath = Path.Combine(folderPath, AssemblyName + ".dll");
            //if (File.Exists(folderPath) == false) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

    }

}


