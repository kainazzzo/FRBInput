using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;

namespace ExternalInputShared
{
    public class Vector3DInput : I3DInput
    {
        private Vector3 _current;
        private Vector3 _previous;

        public float X
        {
            get { return _current.X; }
            set
            {
                _previous.X = _current.X;
                _current.X = value;
            }
        }

        public float Y
        {
            get { return _current.Y; }
            set
            {
                _previous.Y = _current.Y;
                _current.Y = value;
            }
        }

        public float Z
        {
            get { return _current.Z; }
            set
            {
                _previous.Z = _current.Z;
                _current.Z = value;
            }
        }

        public float XVelocity => (_current.X - _previous.X) / TimeManager.SecondDifference;

        public float YVelocity => (_current.Y - _previous.Y) / TimeManager.SecondDifference;
        public float ZVelocity => (_current.Z - _previous.Z) / TimeManager.SecondDifference;
        public float Magnitude => _current.Length();
    }
}