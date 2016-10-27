using System;
using Apache.NMS;
using FlatRedBall.Input;

namespace ExternalInputShared
{
    // TODO: Get this implementation off the laptop.
    public class QueuedPressableInput : IPressableInput, IDisposable
    {
        private readonly QueueConsumer _queueConsumer;
        private bool _previousDown;

        public QueuedPressableInput()
        {
            _queueConsumer = new QueueConsumer("IPressableInput");

            _queueConsumer.Listener += QueueConsumerOnListener;
        }

        private void QueueConsumerOnListener(IMessage message)
        {
            var bytesMessage = (IBytesMessage)message;

            IsDown = bytesMessage.Content[0] == 1;
        }

        public bool IsDown { get; private set; }
        public bool WasJustPressed
        {
            get
            {
                bool retValue;
                if (!_previousDown && IsDown)
                {
                    retValue = true;
                }
                else
                {
                    retValue = false;
                }

                _previousDown = IsDown;

                return retValue;
            }
        }
        public bool WasJustReleased {
            get
            {
                bool retValue;
                if (_previousDown && !IsDown)
                {
                    retValue = true;
                }
                else
                {
                    retValue = false;
                }

                _previousDown = IsDown;

                return retValue;
            }
        }

        public void Dispose()
        {
            _queueConsumer.Dispose();
        }
    }
}