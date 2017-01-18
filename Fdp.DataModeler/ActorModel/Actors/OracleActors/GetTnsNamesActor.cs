using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using System.Collections.ObjectModel;
using System.Linq;

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
            Sender.Tell(new SetTnsNamesMessage(new ObservableCollection<string>(TnsNames.OrderBy(x => x).Distinct().ToList())));
        }

        private void GetTnsNamesFromFile(object obj)
        {
            var TnsNames = _oracleConnectionBuildingService.GetTnsNamesFromFile();
            Sender.Tell(new SetTnsNamesMessage(new ObservableCollection<string>(TnsNames?.OrderBy(x => x).Distinct().ToList())));
        }

    }

    public class TnsNamesTextMessage
    {
    }

    public class TnsNamesFileMessage
    {
    }
}