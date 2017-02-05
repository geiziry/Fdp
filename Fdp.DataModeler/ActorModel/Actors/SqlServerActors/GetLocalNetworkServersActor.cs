using Akka.Actor;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using System.Collections.ObjectModel;

namespace Fdp.DataModeller.ActorModel.Actors.SqlServerActors
{
    public class GetLocalNetworkServersActor : ReceiveActor
    {
        private readonly ISqlServerConnectionBuildingService _sqlServerConnectionBuildingService;

        public GetLocalNetworkServersActor(ISqlServerConnectionBuildingService sqlServerConnectionBuildingService)
        {
            _sqlServerConnectionBuildingService = sqlServerConnectionBuildingService;

            Receive<GetLocalNetworkServersMessage>(message => GetLocalServers());
        }

        private void GetLocalServers()
        {
            Sender.Tell(new SetLocalNetworkServersMessage
                (_sqlServerConnectionBuildingService.GetLocalNetworkServersAsync()));
        }
    }

    internal class GetLocalNetworkServersMessage
    {
    }
    internal class SetLocalNetworkServersMessage
    {
        public SetLocalNetworkServersMessage(ObservableCollection<string> dataSources)
        {
            DataSources = dataSources;
        }

        public ObservableCollection<string> DataSources { get;private set; }
    }
}