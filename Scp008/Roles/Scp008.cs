using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs;
using PlayerStatsSystem;
using Scp008.API;
using UnityEngine;
using YamlDotNet.Serialization;
using PlayerHandlers = Exiled.Events.Handlers.Player;

namespace Scp008.Roles
{
    public sealed class Scp008 : CustomRole
    {
        [YamlIgnore]
        private AhpStat.AhpProcess _shield;

        public override string Name { get; set; } = "SCP-008";

        [YamlIgnore]
        public override uint Id { get; set; } = 8;

        public override string CustomInfo { get; set; } = "SCP-008";

        public override string Description { get; set; } = "Зомби, зараженный SCP-008.";

        public override int MaxHealth { get; set; } = 400;

        public int MaxShield { get; set; } = 300;

        [YamlIgnore]
        public override Vector3 Scale { get; set; } = Vector3.one;

        [YamlIgnore]
        public override RoleType Role { get; set; } = RoleType.Scp0492;

        public override bool RemovalKillsPlayer { get; set; } = true;

        [YamlIgnore]
        public override bool KeepInventoryOnSpawn { get; set; } = false;

        [YamlIgnore]
        public override bool KeepRoleOnDeath { get; set; } = false;

        protected override void RoleAdded(Player player) => SetupShield(player);

        protected override void RoleRemoved(Player player) => player.ReferenceHub.playerStats.GetModule<AhpStat>().ServerKillProcess(_shield.KillCode);

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();

            PlayerHandlers.Died += OnDied;
            PlayerHandlers.Hurting += OnHurting;
            PlayerHandlers.UsedItem += OnUsedItem;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();

            PlayerHandlers.Died -= OnDied;
            PlayerHandlers.Hurting -= OnHurting;
            PlayerHandlers.UsedItem -= OnUsedItem;
        }

        private void SetupShield(Player player)
        {
            player.MaxArtificialHealth = MaxShield;

            _shield = player.ReferenceHub.playerStats.GetModule<AhpStat>().ServerAddProcess(0, 0, 0, 1, 10, true);
            _shield.Limit = _shield.CurrentAmount = MaxShield;
            _shield.DecayRate = -3;
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (Check(ev.Target))
            {
                foreach (Player player in Extensions.GetPlayersInRadius(ev.Target.Position, 3))
                {
                    if (player.UserId == ev.Target.UserId)
                    {
                        return;
                    }

                    Extensions.InfectPlayer(player, 480);
                }

                return;
            }

            if (!Check(ev.Killer))
            {
                return;
            }

            if (ev.Handler.IsSuicide || Random.Range(0, 101) <= 80)
            {
                return;
            }

            AddRole(ev.Target);
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Target.SessionVariables.ContainsKey("Infected"))
            {
                ev.Amount *= 1.05f;
            }
            if (ev.Handler.Type != DamageType.Scp0492) return;
            Extensions.InfectPlayer(ev.Target, 360);
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.SCP500 || !ev.Player.SessionVariables.ContainsKey("Infected"))
            {
                return;
            }

            ev.Player.GameObject.GetComponent<Infected>()?.Reset();
        }
    }
}