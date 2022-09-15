using Exiled.CustomRoles.API.Features;
using UnityEngine;

namespace Scp008.Roles
{
    internal sealed class Scp008 : CustomRole
    {
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
    }
}
