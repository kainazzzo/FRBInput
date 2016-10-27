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

            var allbytes = new byte[sizeof(float)*2];
            byteMessage.Content.CopyTo(allbytes, 0);

            var xbytes = new byte[sizeof(float)];
            var ybytes = new byte[sizeof(float)];
            
            for (int x = 0; x < sizeof(float); ++x)
            {
                xbytes[x] = allbytes[x];
                ybytes[x] = allbytes[x + sizeof(float)];
            }

            X = BitConverter.ToSingle(xbytes, 0);
            Y = BitConverter.ToSingle(ybytes, 0);
        }
    }
}