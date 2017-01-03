using TestStack.White.UIItems;
using TestStack.White.UIItems.Custom;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.Utility;

namespace Fdp.UITest
{
    public class DataModellingWindow : WindowObject
    {
        internal DataModellingWindow(Window window) : base(window)
        {
        }

        private CheckBox AddDataSourceCheckBox => CheckBox("AddDataSource");
        private CheckBox hasTnsCheckBox => CheckBox("hasTns");

        internal void AddDataSource()
        {
            AddDataSourceCheckBox.Click();
        }

        internal void CheckHasTns()
        {
           var tns= window.Get( SearchCriteria.ByAutomationId("hasTns"));

            if (tns != null)
            {
                tns.Click();
            }

            //var TnSCheckBox=
            //    OrcaleConnectionView.Get<CheckBox>(SearchCriteria.ByAutomationId("hasTns"));
            //TnSCheckBox.Click();
        }

    }
}