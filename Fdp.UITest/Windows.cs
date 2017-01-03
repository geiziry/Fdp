using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Utility;

namespace Fdp.UITest
{
    public static class Windows
    {
        private static Application _application;

        public static ShellWindow shellWindow
        {
            get
            {
                Window window = GetWindow("Fdp");
                return new ShellWindow(window);
            }
        }

        public static DataModellingWindow dataModellingWindow
        {
            get
            {
                Window window = GetWindow("Data Sources");
                return new DataModellingWindow(window);
            }
        }

        public static void Init(Application application)
        {
            _application = application;
        }

        private static Window GetWindow(string title)
        {
            return Retry.For(() =>
            _application.GetWindows().First(x => x.Title.Contains(title)),
            TimeSpan.FromSeconds(5));
        }
    }
}

