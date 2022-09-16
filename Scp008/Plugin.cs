using Scp008.Handlers;
using System;

namespace Scp008
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Name => "SCP-008";

        public override string Prefix => "scp_008";

        public override string Author => "Exmetria IT (AlexanderK and rd)";

        public override Version Version { get; } = new (1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new (5, 3, 0);

        public override void OnEnabled()
        {
            PlayerHandler.RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerHandler.UnregisterEvents();

            base.OnDisabled();
        }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}