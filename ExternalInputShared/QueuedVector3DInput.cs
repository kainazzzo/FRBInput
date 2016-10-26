using System;
using Apache.NMS;

namespace ExternalInputShared
{
    public class QueuedVector3DInput : Vector3DInput, IDisposable
    {
        private readonly QueueConsumer _queueConsumer;

        public QueuedVector3DInput()
        {
            _queueConsumer = new QueueConsumer("I3DInput");

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
            Y = BitConverter.ToSingle(byteMessage.Content, sizeof (float));
            Z = BitConverter.ToSingle(byteMessage.Content, sizeof (float)*2);
        }
    }
}