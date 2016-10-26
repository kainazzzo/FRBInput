using System;
using Apache.NMS;

namespace ExternalInputShared
{
    public class QueueConsumer : IDisposable
    {
        private readonly ISession _session;
        private readonly IConnection _connection;
        private readonly IQueue _destination;
        private readonly IMessageConsumer _consumer;

        public QueueConsumer(string destinationName)
        {
            // TODO: Auto discovery
            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            _connection = factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();
            _destination = _session.GetQueue(destinationName);
            _consumer = _session.CreateConsumer(_destination);
            _consumer.Listener += message => Listener?.Invoke(message);
        }

        public void Dispose()
        {
            _consumer.Close();
            _session.DeleteDestination(_destination);
            _session.Close();
            _connection.Stop();
            _connection.Close();
        }

        public event Action<IMessage> Listener;
    }
}