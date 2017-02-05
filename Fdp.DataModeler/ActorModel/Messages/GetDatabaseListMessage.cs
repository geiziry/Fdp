using Fdp.DataAccess.DatabaseSchema;
using System.Data.SqlClient;

namespace Fdp.DataModeller.ActorModel.Messages
{
    internal class GetDatabaseListMessage
    {

        public GetDatabaseListMessage(FdpSqlConnection connection)
        {
            this.Connection = connection;
        }

        public FdpSqlConnection Connection { get; private set; }
    }
}