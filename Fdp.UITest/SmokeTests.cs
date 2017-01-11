using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;

namespace Fdp.UITest
{
    [TestClass()]
    public class SmokeTests:TestsBase
    {
        [TestMethod()]
        public void Can_get_Tns()
        {
            Windows.shellWindow.OpeDataModelling();
            Windows.dataModellingWindow.AddDataSource();
            Windows.dataModellingWindow.CheckHasTns();
        }
    }
}