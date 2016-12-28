using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var SqlServerConnection = new FdpSqlConnection
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
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetDatabaseListAsyncTestInValidDataSource()
        {
            //Arrange
            var SqlServerConnection = new FdpSqlConnection
            {
                DataSource = "MELGEIZIRY1",
                UserName = "geiziry",
                Password = "geiziry3"
            };
            try
            {
                var dbList = await SqlServerConnection.GetDatabaseListAsync();
            }
            catch (Exception ex)
            {
                string message = "The network path was not found";
                Assert.AreEqual(message, GetMessage(ex));
                throw;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetDatabaseListAsyncTestInValidUserName()
        {
            //Arrange
            var SqlServerConnection = new FdpSqlConnection
            {
                DataSource = "MELGEIZIRY",
                UserName = "geiziry1",
                Password = "geiziry3"
            };
            try
            {
                var dbList = await SqlServerConnection.GetDatabaseListAsync();
            }
            catch (Exception ex)
            {
                string message = "Login failed for user 'geiziry1'.";
                Assert.AreEqual(message, GetMessage(ex));
                throw;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetDatabaseListAsyncTestInValidPassword()
        {
            //Arrange
            var SqlServerConnection = new FdpSqlConnection
            {
                DataSource = "MELGEIZIRY",
                UserName = "geiziry",
                Password = "geiziry"
            };
            //Act
            try
            {
                var dbList = await SqlServerConnection.GetDatabaseListAsync();
            }
            catch (Exception ex)
            {
                string message = "Login failed for user 'geiziry'.";
                Assert.AreEqual(message, GetMessage(ex));
                throw;
            }
            //Assert
        }

        [Ignore]
        private string GetMessage(Exception exception)
        {
            var message = exception.InnerException == null ? exception.Message
               : exception.InnerException.Message;

            return message;
        }
    }
}