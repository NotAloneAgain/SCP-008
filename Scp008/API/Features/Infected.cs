using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using UnityEngine;

namespace Scp008.API
{
    [RequireComponent(typeof(ReferenceHub))]
    internal sealed class Infected : MonoBehaviour
    {
        private Timer _timer;
        private ReferenceHub _referenceHub;

        public void Init(float time)
        {
            _referenceHub = GetComponent<ReferenceHub>();
            _timer = gameObject.AddComponent<Timer>();
            _timer.Finished += OnTimerFinished;

            _timer.Init(time);
        }

        public void Reset()
        {
            _referenceHub = null;
            _timer.Finished -= OnTimerFinished;

            Destroy(_timer);
        }

        private void OnTimerFinished()
        {
            var player = Player.Get(_referenceHub);

            player.SessionVariables.Remove("Infected");
            CustomRole.Get(8).AddRole(player);

            Destroy(_timer);
            Destroy(this);
        }
    }
}