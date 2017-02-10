namespace Chimera.Extensions.Logging.Log4Net.Tests
{
    using System;
    using System.Collections.Generic;
    using Chimera.Extensions.Logging.Log4Net;
    using Chimera.Extensions.Logging.Log4Net.Tests.Configuration;
    using log4net;
    using log4net.Core;
    using log4net.Repository;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class ConfigurationLog4NetSettingsTests
    {
        [Fact]
        public void ConfigurationSettingsGlobalContextProperties()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "ConfigFilePath", "test.config" },
                { "Watch", "true" }
            };
            var config = new ConfigurationBuilder()
                .Add(new JsonStringConfigurationSource("{ 'GlobalContextProperties': { 'propertyA': 'valueA', 'propertyB': 'valueB' } }"))
                .AddInMemoryCollection(dict)
                .Build();

            // Act
            var settings = new ConfigurationLog4NetSettings(config);

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(3, settings.GlobalContextProperties.Count);
            Assert.Equal("test.config", settings.ConfigFilePath);
            Assert.True(settings.Watch);
        }
    }
}
