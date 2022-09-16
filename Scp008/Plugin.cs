using System;
using Exiled.CustomRoles.API;

namespace Scp008
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Name => "SCP-008";

        public override string Prefix => "scp_008";

        public override string Author => "Exmetria IT (AlexanderK and rd( TIR? ))";

        public override Version Version { get; } = new (1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new (5, 3, 0);

        public override void OnEnabled()
        {
            Config.Role.Register();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Config.Role.Register();

            base.OnDisabled();
        }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}