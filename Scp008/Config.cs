namespace Scp008
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}