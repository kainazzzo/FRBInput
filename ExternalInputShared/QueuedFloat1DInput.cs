using System;
using Apache.NMS;

namespace ExternalInputShared
{
    public class QueuedFloat1DInput : Float1DInput, IDisposable
    {
        private readonly QueueConsumer _queueConsumer;

        public QueuedFloat1DInput()
        {
            _queueConsumer = new QueueConsumer("I1DInput");

            _queueConsumer.Listener += ConsumerOnListener;
        }

        public void Dispose()
        {
            _queueConsumer.Dispose();
        }

        private void ConsumerOnListener(IMessage message)
        {

            var byteMessage = (IBytesMessage)message;

            Value = BitConverter.ToSingle(byteMessage.Content, 0);
        }
    }
}