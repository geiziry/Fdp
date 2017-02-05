using Akka.Actor;
using Akka.DI.Core;
using Fdp.DataModeller.ActorModel.Actors.SqlServerActors;
using Fdp.DataModeller.ActorModel.Actors.UI;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.DataModeller.ViewModels;
using Fdp.InfraStructure.AkkaHelpers;
using System.Windows;

namespace Fdp.DataModeller.ActorModel.Actors
{
    public class SqlServerCoordinatorActor : ReceiveActor
    {
        public SqlServerConnectionViewModel _viewModel;
        private readonly IActorRef _GetDatabaseListActor;
        private readonly IActorRef _GetLocalNetworkServersActor;
        private readonly IActorRef _progressBarActor;
        private string IsGettingSqlServers;
        private string IsGettingCatalogs;
        public SqlServerCoordinatorActor(SqlServerConnectionViewModel viewModel)
        {
            _progressBarActor = Context.ActorOf(
                Props.Create(() => new ProgressBarActor(viewModel)), ActorPaths.SqlServerCoordinatorActor.Name);

            _GetLocalNetworkServersActor = Context.ActorOf(
               Context.DI().Props<GetLocalNetworkServersActor>());

            _GetDatabaseListActor = Context.ActorOf(
                Context.DI().Props<GetDatabaseListActor>());

            _viewModel = viewModel;

            IsGettingSqlServers = nameof(viewModel.IsGettingSqlServers);
            IsGettingCatalogs = nameof(viewModel.IsGettingCatalogs);

            Receive<GetLocalNetworkServersMessage>(message =>
            {
                SetGettingSqlServerProgress(IsGettingSqlServers);
                _GetLocalNetworkServersActor.Tell(new GetLocalNetworkServersMessage());
            });
            Receive<SetLocalNetworkServersMessage>(message => SetLocalServers(message));

            Receive<GetDatabaseListMessage>(message =>
            {
                SetGettingSqlServerProgress(IsGettingCatalogs);
                _GetDatabaseListActor.Tell(message);
            });
            Receive<SetDatabaseListMessage>(message => SetDatabaseList(message));
        }

        private void SetDatabaseList(SetDatabaseListMessage message)
        {
            SetGettingSqlServerProgress(IsGettingCatalogs);
            _viewModel.Catalogs = message.Catalogs;
        }

        private void SetLocalServers(SetLocalNetworkServersMessage message)
        {
            SetGettingSqlServerProgress(IsGettingSqlServers);
            _viewModel.DataSources = message.DataSources;
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                var Append = string.IsNullOrEmpty(_viewModel.ParentViewModel.ConnectionException) ?
                $"sqlserver: {exception.Message}" : $"\nsqlserver: {exception.Message}";
                _viewModel.ParentViewModel.TextToAppend = Append;
                if (_viewModel.IsGettingCatalogs == Visibility.Visible)
                    SetGettingSqlServerProgress(IsGettingCatalogs);
                if (_viewModel.IsGettingSqlServers == Visibility.Visible)
                    SetGettingSqlServerProgress(IsGettingSqlServers);
                return Directive.Restart;
            });
        }

        private void SetGettingSqlServerProgress(string visibilityProperty)
            => _progressBarActor.Tell(new SetVisibilityPropertyMessage(visibilityProperty));
    }
}