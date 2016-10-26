using System;
using Apache.NMS;

namespace ExternalInputShared
{
    public class QueuedVector2DInput : Vector2DInput, IDisposable
    {
        private readonly ISession _session;
        private readonly IConnection _connection;
        private readonly IQueue _destination;
        private readonly IMessageConsumer _consumer;

        public QueuedVector2DInput()
        {
            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            _connection = factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();
            _destination = _session.GetQueue("I2DInput");
            _consumer = _session.CreateConsumer(_destination);
            _consumer.Listener += ConsumerOnListener;
        }
        public void Dispose()
        {
            _consumer.Close();
            _session.DeleteDestination(_destination);
            _session.Close();
            _connection.Stop();
            _connection.Close();
        }

        private void ConsumerOnListener(IMessage message)
        {

            var byteMessage = (IBytesMessage)message;

            X = BitConverter.ToSingle(byteMessage.Content, 0);
            Y = BitConverter.ToSingle(byteMessage.Content, sizeof(float));
        }
    }
}