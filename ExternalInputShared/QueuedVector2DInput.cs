using System;
using Apache.NMS;

namespace ExternalInputShared
{
    public class QueuedVector2DInput : Vector2DInput, IDisposable
    {
        private readonly QueueConsumer _queueConsumer;

        public QueuedVector2DInput()
        {
            _queueConsumer = new QueueConsumer("I2DInput");

            _queueConsumer.Listener += ConsumerOnListener;
        }

        public void Dispose()
        {
            _queueConsumer.Dispose();
        }

        private void ConsumerOnListener(IMessage message)
        {

            var byteMessage = (IBytesMessage)message;

            X = BitConverter.ToSingle(byteMessage.Content, 0);
            Y = BitConverter.ToSingle(byteMessage.Content, sizeof(float));
        }
    }
}