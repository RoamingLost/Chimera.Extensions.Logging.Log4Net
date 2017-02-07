namespace Chimera.Extensions.Logging.Log4Net.Tests.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Represents a JSON string as an <see cref="IConfigurationSource"/>.
    /// </summary>
    /// <remarks>Based on <a href="https://github.com/aspnet/Configuration/blob/dev/src/Microsoft.Extensions.Configuration.Json/JsonConfigurationSource.cs">JsonConfigurationSource</a>.</remarks>
    /// <seealso cref="Microsoft.Extensions.Configuration.IConfigurationSource" />
    /// <seealso href="https://github.com/aspnet/Configuration/tree/dev/src/Microsoft.Extensions.Configuration.Json">Microsoft.Extensions.Configuration.Json</seealso>
    public class JsonStringConfigurationSource : IConfigurationSource
    {
        private string _json;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringConfigurationSource"/> class.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        public JsonStringConfigurationSource(string json)
        {
            _json = json;
        }

        /// <summary>
        /// Builds the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new JsonStringConfigurationProvider(_json);
        }
    }
}
