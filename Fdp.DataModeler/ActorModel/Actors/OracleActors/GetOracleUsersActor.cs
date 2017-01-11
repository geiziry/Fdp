using System;
using System.Threading.Tasks;
using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;

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
            var OracleUsers = await _oracleConnectionBuildingService.GetOracleUsers(message.Connection);
            Sender.Tell(OracleUsers);
        }
    }
}