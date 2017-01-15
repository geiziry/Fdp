using Fdp.DataAccess.DatabaseSchema;
using Oracle.ManagedDataAccess.Client;

namespace Fdp.DataModeller.ActorModel.Messages
{
    public class GetOracleUsersMessage
    {
        public GetOracleUsersMessage(FdpOracleConnection connection)
        {
            Connection = connection;
        }

        public FdpOracleConnection Connection { get; private set; }
    }
}