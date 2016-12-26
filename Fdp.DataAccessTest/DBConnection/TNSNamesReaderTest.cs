using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fdp.DataAccess.DBConnection.Tests
{
    [TestClass()]
    public class TNSNamesReaderTest
    {
        [TestMethod()]
        public void LoadTNSNamesTest()
        {
            var tnsNamesReader = new TNSNamesReader();

            var homes = tnsNamesReader.GetOracleHomes();
            tnsNamesReader.SetTnsFileText(homes[0], true);
            var tns = tnsNamesReader.LoadTnsNames();
            var datasource = tnsNamesReader.GetDataSource(tns[0]);
        }
    }
}