using System;

namespace Scp008
{
    public sealed class Scp008 : Exiled.API.Features.Plugin<Config>
    {
        public override string Name => "SCP-008";

        public override string Prefix => "scp_008";

        public override string Author => "Exmetria IT (AlexanderK and rd)";

        public override Version Version => new (1, 0, 0);

        public override Version RequiredExiledVersion => new (5, 3, 0);

        public override void OnEnabled()
        {
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}