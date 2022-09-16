namespace Scp008
{
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public Roles.Scp008 Role { get; set; } = new ();
    }
}