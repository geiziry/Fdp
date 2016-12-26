using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fdp.DataAccess.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var tns = tnsNamesReader.LoadTNSNames(homes[0]);
            var datasource = tnsNamesReader.GetDataSource(tns[0]);
        }
    }
}