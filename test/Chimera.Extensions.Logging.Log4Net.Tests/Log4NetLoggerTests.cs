namespace Chimera.Extensions.Logging.Log4Net.Tests
{
    using System;
    using Chimera.Extensions.Logging.Log4Net;
    using log4net;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class Log4NetLoggerTests
    {
        [Fact]
        public void LoggerDebugEnabled()
        {
            // Arrange
            var logMock = new Mock<ILog>();
            logMock.Setup(m => m.IsDebugEnabled).Returns(true);
            var logger = new Log4NetLogger(logMock.Object);

            // Act
            var isEnabled = logger.IsEnabled(LogLevel.Debug);

            // Assert
            Assert.True(isEnabled);
        }

        [Fact]
        public void LoggerDebugDisabled()
        {
            // Arrange
            var logMock = new Mock<ILog>();
            logMock.Setup(m => m.IsDebugEnabled).Returns(false);
            var logger = new Log4NetLogger(logMock.Object);

            // Act
            var isEnabled = logger.IsEnabled(LogLevel.Debug);

            // Assert
            Assert.False(isEnabled);
        }

        // TODO add more
    }
}
