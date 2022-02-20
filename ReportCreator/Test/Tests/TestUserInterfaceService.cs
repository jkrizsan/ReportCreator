using Moq;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ReportCreator.BusinessLogic.Data;
using ReportCreator.BusinessLogic.Services;
using ReportCreator.BusinessLogic.Interfaces;
using ReportCreator.BusinessLogic.Exceptions;

namespace ReportCreator.Tests
{

    [TestFixture]
    public class TestUserInterfaceService
    {
        private AppSettings _appSettings;

        private IUserInterfaceService _userInterfaceService;

        private Mock<IFileWrapper> _fileWrapperMock;

        private Mock<ILogger<UserInterfaceService>> _loggerMock;

        private const string fileName = "input.csv";

        [SetUp]
        public void SetUp()
        {
            _appSettings = new AppSettings()
            {
                SupportedLanguages = new List<SupportedLanguage>
                    {   new SupportedLanguage()
                        {
                            Name = "en",
                            CultureInfo = "en-US",
                            DateFormat = "MM/dd/yyyy"
                        },
                        new SupportedLanguage()
                        {
                            Name = "fi",
                            CultureInfo = "fi-FI",
                            DateFormat = "d.M.yyyy"
                        }
                    }
            };

            _fileWrapperMock = new Mock<IFileWrapper>();
            _fileWrapperMock.Setup(x => x.Exists(fileName)).Returns(() => true);

            _loggerMock = new Mock<ILogger<UserInterfaceService>>();
            _loggerMock.Setup(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()));

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => _loggerMock.Object);

            _userInterfaceService = new UserInterfaceService(mockLoggerFactory.Object, _fileWrapperMock.Object,
                _appSettings);
        }

        [Test]
        public void Test_BuildRequest_OK()
        {
            string[] args = new string[] { ";", "fi", "en", fileName };
            var res = _userInterfaceService.BuildRequest(args);

            Assert.AreEqual(res.FilePath, fileName);

            _loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Convert request built succesfully!")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [Test]
        public void Test_BuildRequest_Exception()
        {
            string[] args = new string[] { ";", string.Empty, string.Empty, fileName };

            Assert.Throws<UserException>(() => { _userInterfaceService.BuildRequest(args); });
        }
    }
}
