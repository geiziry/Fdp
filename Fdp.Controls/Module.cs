using DevExpress.Mvvm.UI;
using Fdp.Controls.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.Controls
{
    public class Module : IModule
    {
        IUnityContainer container;

        public Module(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IViewLocator, MenuViewLocator>();
        }
    }
}
