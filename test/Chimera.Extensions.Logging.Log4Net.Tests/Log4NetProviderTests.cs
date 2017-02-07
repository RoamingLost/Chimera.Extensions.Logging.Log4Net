namespace Chimera.Extensions.Logging.Log4Net.Tests
{
    using System;
    using Chimera.Extensions.Logging.Log4Net;
    using log4net;
    using Moq;
    using Xunit;

    public class Log4NetProviderTests
    {
        [Fact]
        public void ProviderGeneratesLogger()
        {
            // Arrange
            var logMock = new Mock<ILog>();
            var containerMock = new Mock<ILog4NetContainer>();
            containerMock.Setup(m => m.IsInitialized).Returns(true);
            containerMock.Setup(m => m.GetLog(It.IsAny<string>())).Returns(logMock.Object);

            // Act
            var provider = new Log4NetProvider(containerMock.Object);
            var logger = provider.CreateLogger("test");

            // Assert
            Assert.NotNull(logger);
        }
    }
}
