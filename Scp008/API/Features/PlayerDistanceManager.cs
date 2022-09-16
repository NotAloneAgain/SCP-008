using PlayerStatsSystem;
using UnityEngine;
using System;

namespace Scp008.API
{
    public sealed class PlayerDistanceManager : MonoBehaviour
    {
        private HealthStat _health;
        private float _outDistance;
        private float _inDistance;
        private float _timer;

        private void Start() => _health = GetComponent<HealthStat>();

        private void FixedUpdate()
        {
            _timer -= Time.fixedUnscaledDeltaTime;

            if (_timer > 0)
            {
                return;
            }

            bool doctorInDistance = Extensions.IsScp049InRadius(transform.position, 8);
            float distance = Mathf.Clamp(Extensions.DistanceToScp049(transform.position), 0, 40);

            if (doctorInDistance)
            {
                if (Mathf.RoundToInt(_outDistance) > 0)
                {
                    _outDistance = Mathf.Max(_outDistance - 0.5f * (9 - distance), 0);
                }
                else
                {
                    _inDistance++;
                }
            }
            else
            {
                if (Mathf.RoundToInt(_inDistance) > 0)
                {
                    _inDistance = Mathf.Max(_inDistance - 1 * (distance / 10), 0);

                    return;
                }

                _outDistance++;
            }

            if (_outDistance == 0 && Mathf.Round(_inDistance) > 2)
            {
                if (_health.CurValue >= _health.MaxValue)
                {
                    return;
                }

                float amount = 1 * (9 - Mathf.RoundToInt(distance));

                if (_health.CurValue + amount >= _health.MaxValue)
                {
                    _health.CurValue = _health.MaxValue;

                    return;
                }

                _health.CurValue += amount;
            }
            else if (_inDistance == 0 && Mathf.Round(_outDistance) > 4)
            {
                float damage = 1 * _outDistance;

                if (_health.CurValue - damage >= _health.MaxValue)
                {
                    _health.CurValue = _health.MaxValue;

                    return;
                }

                _health.CurValue -= damage;
            }

            _timer = 1;
        }
    }
}