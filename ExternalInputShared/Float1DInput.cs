using FlatRedBall;
using FlatRedBall.Input;

namespace ExternalInputShared
{
    public class Float1DInput : I1DInput
    {
        private float _current;
        private float _previous;

        public float Value
        {
            get { return _current; }
            set
            {
                _previous = _current;
                _current = value;
            }
        }

        public float Velocity => _current - _previous/TimeManager.SecondDifference;
    }
}