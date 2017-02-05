using Akka.Actor;
using Akka.DI.Core;
using Fdp.DataModeller.ActorModel.Actors.OracleActors;
using Fdp.DataModeller.ActorModel.Actors.UI;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.DataModeller.ViewModels;
using Fdp.InfraStructure.AkkaHelpers;
using System.Windows;

namespace Fdp.DataModeller.ActorModel.Actors
{
    public class OracleCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _progressBarActor;
        private readonly OracleConnectionViewModel _viewModel;
        private string isGettingUsers;
        public OracleCoordinatorActor(OracleConnectionViewModel viewModel)
        {
            _progressBarActor = Context.ActorOf(
                        Props.Create(() => new ProgressBarActor(viewModel)), ActorPaths.OracleConnProgressBarActor.Name);

            _getOracleUsersActor = Context.ActorOf(
                Context.DI().Props<GetOracleUsersActor>());

            Context.ActorOf(Context.DI().Props<GetTnsNamesActor>(), ActorPaths.GetTnsNamesActor.Name);

            Receive<GetOracleUsersMessage>(message => GetOracleUsers(message));
            Receive<SetOracleUsersMessage>(message => SetOracleUsers(message));

            Receive<SetTnsNamesMessage>(message => SetTnsNames(message));
            _viewModel = viewModel;
            isGettingUsers = nameof(_viewModel.IsGettingUsers);
        }

        public IActorRef _getOracleUsersActor { get; private set; }

        protected override SupervisorStrategy SupervisorStrategy() =>
            new OneForOneStrategy(exception =>
                {
                    var Append = string.IsNullOrEmpty(_viewModel.ParentViewModel.ConnectionException) ?
                    $"oracle: {exception.Message}" : $"\noracle: {exception.Message}";
                    _viewModel.ParentViewModel.TextToAppend = Append;
                    if (_viewModel.IsGettingUsers == Visibility.Visible)
                        _progressBarActor.Tell(new SetVisibilityPropertyMessage(isGettingUsers));
                    return Directive.Restart;
                });

        private void GetOracleUsers(GetOracleUsersMessage message)
        {
            _progressBarActor.Tell(new SetVisibilityPropertyMessage(isGettingUsers));
            _getOracleUsersActor.Tell(message);
        }

        private void SetOracleUsers(SetOracleUsersMessage message)
        {
            _progressBarActor.Tell(new SetVisibilityPropertyMessage(isGettingUsers));
            _viewModel.UsersList = message.OracleUsers;
        }

        private void SetTnsNames(SetTnsNamesMessage message)
        {
            _viewModel.TnsNames = message.TnsNames;
        }
    }
}