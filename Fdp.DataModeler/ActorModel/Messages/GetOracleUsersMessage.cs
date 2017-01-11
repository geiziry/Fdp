using Oracle.ManagedDataAccess.Client;

namespace Fdp.DataModeller.ActorModel.Messages
{
    public class GetOracleUsersMessage
    {
        public GetOracleUsersMessage(OracleConnection connection)
        {
            Connection = connection;
        }

        public OracleConnection Connection { get; private set; }
    }
}