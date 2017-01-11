using Akka.Actor;
using Akka.DI.Core;
using Fdp.DataModeller.ActorModel.Actors.OracleActors;
using Fdp.DataModeller.ActorModel.Messages;
using System;
using System.Threading.Tasks;

namespace Fdp.DataModeller.ActorModel.Actors
{
    public class OracleCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _progressBarActor;
        public IActorRef _tnsNamesActor { get; private set; }
        public IActorRef _getOracleUsersActor { get; private set; }

        public OracleCoordinatorActor(IActorRef progressBarActor)
        {
            _progressBarActor = progressBarActor;

            _getOracleUsersActor = Context.ActorOf(
                Context.DI().Props<GetOracleUsersActor>());

            _tnsNamesActor = Context.ActorOf(
                Context.DI().Props<GetTnsNamesActor>());

            Receive<GetOracleUsersMessage>(message => GetOracleUsers(message));
        }

        private void GetOracleUsers(GetOracleUsersMessage message)
        {
            _progressBarActor.Tell(new SetVisibilityPropertyMessage("IsGettingUsers"));
            _getOracleUsersActor.Tell(message);
        }
    }
}