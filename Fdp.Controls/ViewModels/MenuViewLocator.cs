using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.Controls.ViewModels
{
    public class MenuViewLocator : IViewLocator
    {
        public object ResolveView(string name)
        {
            if (name == "DataModellingView")
            {
                Assembly assembly = Assembly.LoadFrom(@"Fdp.DataModeller.dll");
                Type DataModellingView = assembly.GetType("Fdp.DataModeller.Views.DataModellingView");
                return Activator.CreateInstance(DataModellingView);
            }
            return null;
        }

        public Type ResolveViewType(string name)
        {
            throw new NotImplementedException();
        }
    }
}
