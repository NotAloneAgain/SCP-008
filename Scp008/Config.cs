namespace Scp008
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <summary>
    /// Plugin config class.
    /// </summary>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        [Description("")]
        public bool IsEnabled { get; set; } = true;
    }
}