using UnityEngine;
using static Exiled.Events.Events;

namespace Scp008.API
{
    internal sealed class Timer : MonoBehaviour
    {
        internal event CustomEventHandler Finished;

        private float _time;
        private bool _isFinished;
        private bool _isStarted;

        internal void Init(float time)
        {
            _time = time;
            _isStarted = true;
        }

        private void FixedUpdate()
        {
            if (_isFinished || !_isStarted) return;
            _time -= Time.fixedDeltaTime;

            if (_time <= 0)
            {
                _isFinished = true;
                Finished?.Invoke();
            }
        }
    }
}