using ElderlyCare.Domain.Robot;
using Opc.Ua;
using Opc.Ua.Client;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using System.Reactive.Linq;

namespace ElderlyCare.Worker
{
    public class RobotAgent
    {
        private readonly string _opcUrl;
        private readonly Guid _robotId;
        private readonly string _robotName;

        private OpcApplication _application;
        private Session _session;
        private RobotMonitor _monitor;

        public IObservable<RobotStateChangedEvent> StateChanges { get; private set; }

        public RobotAgent(string opcUrl, Guid robotId, string robotName)
        {
            _opcUrl = opcUrl;
            _robotId = robotId;
            _robotName = robotName;

        }

        public Task ConnectAsync()
        {
            _application = new OpcApplication("ElderlyCareClient");

            // Retry with exponential backoff for resilent opc connection handling
            var connectionPolicy = RetryPolicy
                .Handle<Opc.Ua.ServiceResultException>()
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
                    medianFirstRetryDelay: TimeSpan.FromSeconds(5), 
                    retryCount: 5));

            return connectionPolicy.ExecuteAsync(TryConnect);
        }

        private Task TryConnect()
        {
            _session = _application.CreateSession(_opcUrl);

            var anonymousIdentity = new UserIdentity(new AnonymousIdentityToken());
            _session.Open(Guid.NewGuid().ToString(), anonymousIdentity);

            _monitor = new RobotMonitor(_session, _robotId, _robotName);

            StateChanges = Observable
                .FromEvent<RobotStateChangedEvent>(
                    x => _monitor.StateChanged += x,
                    x => _monitor.StateChanged -= x)
                .Sample(TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public async Task DisconnectAsync()
        {
            _monitor.Delete();
            await _session?.CloseAsync();
        }
    }
}
