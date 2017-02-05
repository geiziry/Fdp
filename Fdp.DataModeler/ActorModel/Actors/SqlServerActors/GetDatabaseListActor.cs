using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;

namespace Fdp.DataModeller.ActorModel.Actors.SqlServerActors
{
    public class GetDatabaseListActor : ReceiveActor
    {
    private readonly ISqlServerConnectionBuildingService _sqlServerConnectionBuildingService;
        public GetDatabaseListActor(ISqlServerConnectionBuildingService sqlServerConnectionBuildingService)
        {
            _sqlServerConnectionBuildingService = sqlServerConnectionBuildingService;

            ReceiveAsync<GetDatabaseListMessage>(message => GetDatabaseList(message));
        }

        private async Task GetDatabaseList(GetDatabaseListMessage message)
        {
            Sender.Tell(new SetDatabaseListMessage(
                await _sqlServerConnectionBuildingService.GetDatabaseListAsync(message.Connection.Conn)));
        }

    }

    internal class SetDatabaseListMessage
    {
        public SetDatabaseListMessage(ObservableCollection<string> catalogs)
        {
            Catalogs = catalogs;
        }

        public ObservableCollection<string> Catalogs { get; private set; }
    }
}