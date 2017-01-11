using Akka.Actor;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;

namespace Fdp.DataModeller.ActorModel.Actors.OracleActors
{
    public class GetTnsNamesActor : ReceiveActor
    {
        private readonly IOracleConnectionBuildingService _oracleConnectionBuildingService;

        public GetTnsNamesActor(IOracleConnectionBuildingService oracleConnectionBuildingService)
        {
            _oracleConnectionBuildingService = oracleConnectionBuildingService;
            Receive<TnsNamesTextMessage>(null, GetTnsNames);
            Receive<TnsNamesFileMessage>(null, GetTnsNamesFromFile);
        }

        private void GetTnsNames(object obj)
        {
            var TnsNames = _oracleConnectionBuildingService.GetTnsNames();
            Sender.Tell(TnsNames);
        }

        private void GetTnsNamesFromFile(object obj)
        {
            var TnsNames = _oracleConnectionBuildingService.GetTnsNamesFromFile();
            Sender.Tell(TnsNames);
        }

    }

    public class TnsNamesTextMessage
    {
    }

    public class TnsNamesFileMessage
    {
    }
}