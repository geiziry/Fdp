using Akka.Actor;
using Akka.DI.Core;
using Fdp.DataModeller.ActorModel.Actors.OracleActors;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.DataModeller.ViewModels;
using System.Windows;

namespace Fdp.DataModeller.ActorModel.Actors
{
    public class OracleCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _progressBarActor;
        private readonly OracleConnectionViewModel _viewModel;

        public OracleCoordinatorActor(OracleConnectionViewModel viewModel,
            IActorRef progressBarActor)
        {
            _progressBarActor = progressBarActor;

            _getOracleUsersActor = Context.ActorOf(
                Context.DI().Props<GetOracleUsersActor>());

            Context.ActorOf(Context.DI().Props<GetTnsNamesActor>(), "TnsNames");

            Receive<GetOracleUsersMessage>(message => GetOracleUsers(message));
            Receive<SetOracleUsersMessage>(message => SetOracleUsers(message));

            Receive<SetTnsNamesMessage>(message => SetTnsNames(message));
            _viewModel = viewModel;
        }

        public IActorRef _getOracleUsersActor { get; private set; }

        protected override SupervisorStrategy SupervisorStrategy() =>
            new OneForOneStrategy(exception =>
                {
                    var Append = string.IsNullOrEmpty(_viewModel.ParentViewModel.ConnectionException) ?
                    $"oracle: {exception.Message}" : $"\noracle: {exception.Message}";
                    _viewModel.ParentViewModel.TextToAppend = Append;
                    if (_viewModel.IsGettingUsers == Visibility.Visible)
                        _progressBarActor.Tell(new SetVisibilityPropertyMessage("IsGettingUsers"));
                    return Directive.Restart;
                });

        private void GetOracleUsers(GetOracleUsersMessage message)
        {
            _progressBarActor.Tell(new SetVisibilityPropertyMessage("IsGettingUsers"));
            _getOracleUsersActor.Tell(message);
        }

        private void SetOracleUsers(SetOracleUsersMessage message)
        {
            _progressBarActor.Tell(new SetVisibilityPropertyMessage("IsGettingUsers"));
            _viewModel.UsersList = message.OracleUsers;
        }

        private void SetTnsNames(SetTnsNamesMessage message)
        {
            _viewModel.TnsNames = message.TnsNames;
        }

        protected override void PostStop()
        {
            base.PostStop();
            _viewModel.IsCoordinatorActorAlive = false;
        }
    }
}