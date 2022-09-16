using Exiled.API.Features;
using UnityEngine;
using Scp008.API;
using System.Collections.Generic;
using System.Linq;
using PlayerStatsSystem;

namespace Scp008
{
    internal static class Extensions
    {
        public static IEnumerable<Player> GetPlayersInRadius(Vector3 position, float radius)
        {
            return Player.Get(player => Vector3.Distance(player.Position, position) <= radius);
        }

        public static bool IsScp049InRadius(Vector3 position, float radius)
        {
            return GetPlayersInRadius(position, radius).Count(player => player.Role == RoleType.Scp049) > 0;
        }

        public static float DistanceToScp049(Vector3 position)
        {
            return Player.Get(RoleType.Scp049).Select(player => Vector3.Distance(player.Position, position)).OrderBy(distance => distance).First();
        }

        public static void InfectPlayer(Player player, float time)
        {
            if (player.SessionVariables.ContainsKey("infected") || player.IsScp) return;
            var ahpStat = player.ReferenceHub.playerStats.GetModule<AhpStat>();
            var infected = player.GameObject.AddComponent<Infected>();

            ahpStat.KillAllProcess();
            ahpStat.ServerAddProcess(0, 75, 1.2f, 0.7f, 0, false);
            infected.Init(time);
            player.SessionVariables.Add("infected", true);
        }

        public static void CancelInfection(Player player)
        {
            if (player.GameObject.TryGetComponent(out Infected infected))
            {
                infected.Reset();
                Object.Destroy(infected);
            }

            player.ReferenceHub.playerStats.GetModule<AhpStat>().KillAllProcess();
            player.SessionVariables.Remove("infected");
        }

        private static void KillAllProcess(this AhpStat ahpStat)
        {
            foreach (var process in ahpStat?._activeProcesses)
            {
                ahpStat.ServerKillProcess(process.KillCode);
            }
        }
    }
}