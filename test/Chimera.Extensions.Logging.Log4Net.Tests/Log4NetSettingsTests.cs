namespace Chimera.Extensions.Logging.Log4Net.Tests
{
    using System;
    using Chimera.Extensions.Logging.Log4Net;
    using log4net;
    using log4net.Core;
    using log4net.Repository;
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
    }
}
