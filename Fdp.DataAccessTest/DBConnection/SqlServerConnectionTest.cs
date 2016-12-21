using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema.Tests
{
    [TestClass()]
    public class SqlServerConnectionTest
    {
        [TestMethod()]
        public async Task GetDatabaseListAsyncTestValid()
        {
            //Arrange
            var SqlServerConnection = new SqlServerConnection
            {
                DataSource = "MELGEIZIRY",
                UserName = "geiziry",
                Password = "geiziry3"
            };
            var expected = 9;
            //Act
            var dbList = await SqlServerConnection.GetDatabaseListAsync();
            var actual = dbList.Count;
            //Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task GetDatabaseListAsyncTestInValidDataSource()
        {
            //Arrange
            var SqlServerConnection = new SqlServerConnection
            {
                DataSource = "MELGEIZIRY1",
                UserName = "geiziry",
                Password = "geiziry3"
            };
            var expected = 9;
            //Act
            var dbList = await SqlServerConnection.GetDatabaseListAsync();
            var actual = dbList.Count;
            //Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task GetDatabaseListAsyncTestInValidUserName()
        {
            //Arrange
            var SqlServerConnection = new SqlServerConnection
            {
                DataSource = "MELGEIZIRY",
                UserName = "geiziry1",
                Password = "geiziry3"
            };
            var expected = 9;
            //Act
            var dbList = await SqlServerConnection.GetDatabaseListAsync();
            var actual = dbList.Count;
            //Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task GetDatabaseListAsyncTestInValidPassword()
        {
            //Arrange
            var SqlServerConnection = new SqlServerConnection
            {
                DataSource = "MELGEIZIRY",
                UserName = "geiziry",
                Password = "geiziry"
            };
            var expected = 9;
            //Act
            var dbList = await SqlServerConnection.GetDatabaseListAsync();
            var actual = dbList.Count;
            //Assert

            Assert.AreEqual(expected, actual);
        }

    }
}