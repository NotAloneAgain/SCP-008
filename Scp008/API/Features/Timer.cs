using Exiled.Events.Extensions;
using UnityEngine;
using static Exiled.Events.Events;

namespace Scp008.API
{
    public sealed class Timer : MonoBehaviour
    {
        private float _time;
        private bool _isFinished;
        private bool _isStarted;

        public event CustomEventHandler Finished;

        public bool InProgress => !_isFinished && _isStarted;

        public void Init(float time)
        {
            _time = time;
            _isStarted = true;
        }

        private void FixedUpdate()
        {
            if (InProgress)
            {
                return;
            }

            _time -= Time.fixedDeltaTime;

            if (Mathf.RoundToInt(_time) <= 0)
            {
                _isFinished = true;
                Finished.InvokeSafely();
            }
        }
    }
}