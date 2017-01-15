using System;
using System.Threading.Tasks;
using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using System.Collections.ObjectModel;

namespace Fdp.DataModeller.ActorModel.Actors.OracleActors
{
    public class GetOracleUsersActor : ReceiveActor
    {
        private readonly IOracleConnectionBuildingService _oracleConnectionBuildingService;

        public GetOracleUsersActor(IOracleConnectionBuildingService oracleConnectionBuildingService)
        {
            _oracleConnectionBuildingService = oracleConnectionBuildingService;

            ReceiveAsync<GetOracleUsersMessage>(message => GetOracleUsers(message));
        }

        private async Task GetOracleUsers(GetOracleUsersMessage message)
        {
                var OracleUsers=new ObservableCollection<string>();
            try
            {
                OracleUsers = await _oracleConnectionBuildingService.GetOracleUsers(message.Connection.Conn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Sender.Tell(new SetOracleUsersMessage (OracleUsers));
        }
    }
}