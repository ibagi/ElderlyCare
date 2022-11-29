using ElderlyCare.Domain.Robot;
using Opc.Ua;
using Opc.Ua.Client;

namespace ElderlyCare.Workers.RobotHandler
{
    public class RobotMonitor
    {
        private readonly Subscription _subscription;
        public RawRobotState State = new();

        public event Action<RobotStateChangedEvent> StateChanged;

        public RobotMonitor(Session session, Guid robotId, string robotName)
        {
            _subscription = new Subscription(session.DefaultSubscription)
            {
                DisplayName = robotName,
            };

            var statusMonitor = new MonitoredItem
            {
                DisplayName = $"{robotName}/state",
                StartNodeId = new NodeId($"ns=2;s=/Robots/{robotName}.status"),
                AttributeId = Attributes.Value,
            };

            var positionXMonitor = new MonitoredItem
            {
                DisplayName = $"{robotName}/pos_x",
                StartNodeId = new NodeId($"ns=2;s=/Robots/{robotName}.pos_x"),
                AttributeId = Attributes.Value
            };

            var positionYMonitor = new MonitoredItem
            {
                DisplayName = $"{robotName}/pos_y",
                StartNodeId = new NodeId($"ns=2;s=/Robots/{robotName}.pos_y"),
                AttributeId = Attributes.Value
            };

            statusMonitor.Notification += (o, e) =>
            {
                var value = GetRawValue(e);
                State["Status"] = value;
                RaiseStateChangedEvent(robotId, robotName);
            };

            positionXMonitor.Notification += (o, e) =>
            {
                var value = double.Parse(GetRawValue(e));
                State["PosX"] = value;
                RaiseStateChangedEvent(robotId, robotName);
            };

            positionYMonitor.Notification += (o, e) =>
            {
                var value = double.Parse(GetRawValue(e));
                State["PosY"] = value;
                RaiseStateChangedEvent(robotId, robotName);
            };

            _subscription.AddItem(statusMonitor);
            _subscription.AddItem(positionXMonitor);
            _subscription.AddItem(positionYMonitor);

            session.AddSubscription(_subscription);
            _subscription.Create();
        }

        public void Delete()
        {
            _subscription?.Delete(silent: true);
        }

        private void RaiseStateChangedEvent(Guid robotId, string robotName)
        {
            StateChanged?.Invoke(new RobotStateChangedEvent(
                robotId,
                robotName,
                State.Clone()));
        }

        private static string GetRawValue(MonitoredItemNotificationEventArgs e)
        {
            var notification = e.NotificationValue as MonitoredItemNotification;
            var rawValue = notification?.Value?.Value?.ToString();

            if (rawValue == null)
            {
                throw new ArgumentException("rawValue");
            }

            return rawValue;
        }
    }
}
