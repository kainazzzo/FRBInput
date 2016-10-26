using System;
using FlatRedBall.Input;

namespace ExternalInputShared
{
    // TODO: Get this implementation off the laptop.
    public class QueuedPressableInput : IPressableInput, IDisposable
    {
        private readonly QueueConsumer _queueConsumer;

        public QueuedPressableInput()
        {
            _queueConsumer = new QueueConsumer("IPressableInput");
        }

        public bool IsDown { get; }
        public bool WasJustPressed { get; }
        public bool WasJustReleased { get; }

        public void Dispose()
        {
            _queueConsumer.Dispose();
        }
    }
}