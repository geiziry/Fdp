using Fdp.UITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace Fdp.UITest
{
    public class ShellWindow:WindowObject
    {
        internal ShellWindow(Window window):base(window)
        {
        }

        private Button Databtn => Button("Databtn");

        internal void OpeDataModelling()
        {
            Databtn.Click();
        }
    }
}
