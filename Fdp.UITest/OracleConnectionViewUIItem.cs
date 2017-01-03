using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Custom;

namespace Fdp.UITest
{
    [ControlTypeMapping(CustomUIItemType.Custom,TestStack.White.UIItems.WindowsFramework.Wpf)]
    public class OracleConnectionViewUIItem:CustomUIItem
    {
        public OracleConnectionViewUIItem(
            AutomationElement automationElement,
            ActionListener actionListener):base(automationElement,actionListener)
        {
        }

        protected OracleConnectionViewUIItem()
        {
        }
    }
}
