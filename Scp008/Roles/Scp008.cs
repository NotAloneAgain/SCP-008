using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs;
using PlayerStatsSystem;
using Scp008.API;
using UnityEngine;

using PlayerHandlers = Exiled.Events.Handlers.Player;

namespace Scp008.Roles
{
    internal sealed class Scp008 : CustomRole
    {
        private AhpStat.AhpProcess _shield;

        public override string Name { get; set; } = "SCP-008";

        public override uint Id { get; set; } = 8;

        public override string CustomInfo { get; set; } = "SCP-008";

        public override string Description { get; set; } = "Зомби, зараженный SCP-008.";

        public override int MaxHealth { get; set; } = 400;

        public override Vector3 Scale { get; set; } = Vector3.one;

        public override RoleType Role { get; set; } = RoleType.Scp0492;

        public override bool RemovalKillsPlayer { get; set; } = false;

        public override bool KeepInventoryOnSpawn { get; set; } = false;

        public override bool KeepRoleOnDeath { get; set; } = false;

        protected override void RoleAdded(Player player)
        {
            SetupShield(player);
            
        }

        protected override void RoleRemoved(Player player)
        {
            player.ReferenceHub.playerStats.GetModule<AhpStat>().ServerKillProcess(_shield.KillCode);
        }

        protected override void SubscribeEvents()
        {
            PlayerHandlers.Died += OnDied;
            PlayerHandlers.Hurting += OnHurting;
            PlayerHandlers.UsedItem += OnUsedItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            PlayerHandlers.Died -= OnDied;
            PlayerHandlers.Hurting -= OnHurting;
            PlayerHandlers.UsedItem -= OnUsedItem;

            base.UnsubscribeEvents();
        }

        private void SetupShield(Player player)
        {
            player.MaxArtificialHealth = 300;

            _shield = player.ReferenceHub.playerStats.GetModule<AhpStat>().ServerAddProcess(0, 0, 0, 1, 10, true);
            _shield.Limit = _shield.CurrentAmount = 300;
            _shield.DecayRate = -3;
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (Check(ev.Target))
            {
                foreach (var player in Extensions.GetPlayersInRadius(ev.Target.Position, 2f))
                    Extensions.InfectPlayer(player, 480);
            }

            if (ev.Handler.IsSuicide || Random.Range(0, 1f) > 0.8f) return;
            AddRole(ev.Target);
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Target.SessionVariables.ContainsKey("infected"))
            {
                ev.Amount *= 1.05f;
            }
            if (ev.Handler.Type != DamageType.Scp0492) return;
            Extensions.InfectPlayer(ev.Target, 360);
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.SCP500 || !ev.Player.SessionVariables.ContainsKey("infected")) return;
            ev.Player.GameObject.GetComponent<Infected>()?.Reset();
        }
    }
}