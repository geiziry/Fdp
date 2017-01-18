using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.InfraStructure.AkkaHelpers
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData OracleCoordinatorActor = new ActorMetaData("OracleCoordinator");
        public static readonly ActorMetaData GetOracleUsersActor = new ActorMetaData("OracleUsers", OracleCoordinatorActor);
        public static readonly ActorMetaData GetTnsNamesActor = new ActorMetaData("TnsNames", OracleCoordinatorActor);
        public static readonly ActorMetaData ProgressBarActor = new ActorMetaData("ProgressBar", OracleCoordinatorActor);
    }
}
