namespace Chimera.Extensions.Logging.Log4Net.Tests
{
    using System;
    using System.Collections.Generic;
    using Chimera.Extensions.Logging.Log4Net;
    using log4net;
    using log4net.Core;
    using log4net.Repository;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class Log4NetSettingsTests
    {
        [Fact]
        public void VerifyILoggerToILog()
        {
            // Arrange
            var repositoryMock = new Mock<ILoggerRepository>();
            repositoryMock.Setup(m => m.LevelMap).Returns(new LevelMap());
            var loggerMock = new Mock<log4net.Core.ILogger>();
            loggerMock.Setup(m => m.IsEnabledFor(It.IsAny<Level>())).Returns(true);
            loggerMock.Setup(m => m.Repository).Returns(repositoryMock.Object);

            // Act
            var log = new LogImpl(loggerMock.Object);
            log.Debug("This is a test");

            // Assert
            loggerMock.Verify(m => m.Log(It.IsAny<Type>(), It.IsAny<Level>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Fact]
        public void LoggerFactoryLogMessage()
        {
            // Arrange
            var logMock = new Mock<ILog>();
            logMock.Setup(m => m.IsDebugEnabled).Returns(true);
            var containerMock = new Mock<ILog4NetContainer>();
            containerMock.Setup(m => m.IsInitialized).Returns(true);
            containerMock.Setup(m => m.GetLog(It.IsAny<string>())).Returns(logMock.Object);

            // Act
            var factory = new LoggerFactory();
            factory.AddProvider(new Log4NetProvider(containerMock.Object));
            var logger = factory.CreateLogger("test");
            logger.LogDebug("Test Message");

            // Assert
            logMock.Verify(m => m.Debug(It.IsAny<object>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Fact]
        public void SettingsDefault()
        {
            // Arrange


            // Act
            var settings = new Log4NetSettings();

            // Assert
            Assert.Equal(1, settings.GlobalContextProperties.Count);
            Assert.Equal("log4net.config", settings.ConfigFilePath);
            Assert.False(settings.Watch);
        }

        [Fact]
        public void SettingsFromConfiguration()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "ConfigFilePath", "test.config" },
                { "Watch", "true" }
            };
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();

            // Act
            var settings = config.Get<Log4NetSettings>();

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(1, settings.GlobalContextProperties.Count);
            Assert.Equal("test.config", settings.ConfigFilePath);
            Assert.True(settings.Watch);
        }
    }
}
