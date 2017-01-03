using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;

namespace Fdp.UITest
{
    [TestClass()]
    public class SmokeTests
    {
        [TestMethod()]
        public void Can_get_Tns()
        {
            Application application = Application.Launch(
                @"F:\KOC Development Projects\Dashboard\Fdp\Fdp.UI\bin\Debug\Fdp.UI.exe");
            Windows.Init(application);
            Windows.shellWindow.OpeDataModelling();
            Windows.dataModellingWindow.AddDataSource();
            Windows.dataModellingWindow.CheckHasTns();
        }
    }
}