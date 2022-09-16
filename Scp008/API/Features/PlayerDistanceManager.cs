using PlayerStatsSystem;
using UnityEngine;

namespace Scp008.API
{
    internal sealed class PlayerDistanceManager : MonoBehaviour
    {
        private HealthStat _healthStat;
        private float _timer;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            _timer -= Time.fixedUnscaledDeltaTime;
            if (_timer > 0) return;

            if (Extensions.IsScp049InRadius(transform.position, 8))
            {
                _healthStat.CurValue ;
            }
            else
            {
                _healthStat.CurValue -= 1 * Extensions.DistanceToScp049(transform.position);
            }

            _timer = 1;
        }
    }
}