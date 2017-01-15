using System.Collections.ObjectModel;

namespace Fdp.DataModeller.ActorModel.Messages
{
    public class SetOracleUsersMessage
    {
        public SetOracleUsersMessage(ObservableCollection<string> oracleUsers)
        {
            OracleUsers = oracleUsers;
        }

        public ObservableCollection<string> OracleUsers { get; private set; }
    }
}