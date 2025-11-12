using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Application.TransverseServices.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.SocialNetworks;
using Vito.Transverse.Identity.Entities.Enums;
using  Vito.Transverse.Identity.Infrastructure.Models;

namespace Vito.Transverse.Identity.UnitTests.BAL;

public class SocialNetworkServiceTest
{
    private Mock<ISocialNetworksRepository> _socialNetworksRepositoryMock;
    private Mock<ICultureService> _cultureServiceMock;
    private Mock<ICachingServiceMemoryCache> _cachingServiceMock;
    private Mock<ILogger<SocialNetworkService>> _loggerMock;
    private Mock<IOptions<EmailSettingsOptions>> _emailSettingsOptionsMock;
    private SocialNetworkService _service;
    private EmailSettingsOptions _emailSettingsOptions;

    [SetUp]
    public void SetUp()
    {
        _socialNetworksRepositoryMock = new Mock<ISocialNetworksRepository>();
        _cultureServiceMock = new Mock<ICultureService>();
        _cachingServiceMock = new Mock<ICachingServiceMemoryCache>();
        _loggerMock = new Mock<ILogger<SocialNetworkService>>();
        _emailSettingsOptions = new EmailSettingsOptions
        {
            SenderEmail = "sender@test.com",
            UserName = "user",
            Password = "pass",
            ServerName = "smtp.test.com",
            Port = 25,
            EnableSsl = false
        };
        _emailSettingsOptionsMock = new Mock<IOptions<EmailSettingsOptions>>();
        _emailSettingsOptionsMock.Setup(x => x.Value).Returns(_emailSettingsOptions);

        _service = new SocialNetworkService(
            _socialNetworksRepositoryMock.Object,
            _cultureServiceMock.Object,
            _cachingServiceMock.Object,
            _loggerMock.Object,
            _emailSettingsOptionsMock.Object
        );
    }

    [Test]
    public async Task GetNotificationTemplateListAsync_ReturnsFromCache_WhenCacheExists()
    {
        var templates = new List<NotificationTemplateDTO> { new NotificationTemplateDTO { Id = 1 } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Returns(templates);

        var result = await _service.GetNotificationTemplateListAsync();

        Assert.That(result, Is.EqualTo(templates));
        _socialNetworksRepositoryMock.Verify(r => r.GetNotificationTemplateListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<NotificationTemplate, bool>>>(), null), Times.Never);
    }

    [Test]
    public async Task GetNotificationTemplateListAsync_ReturnsFromRepository_WhenCacheIsNull()
    {
        var templates = new List<NotificationTemplateDTO> { new NotificationTemplateDTO { Id = 2 } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Returns((List<NotificationTemplateDTO>)null);
        _socialNetworksRepositoryMock.Setup(r => r.GetNotificationTemplateListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<NotificationTemplate, bool>>>(), null))
            .ReturnsAsync(templates);

        var result = await _service.GetNotificationTemplateListAsync();

        Assert.That(result, Is.EqualTo(templates));
        _cachingServiceMock.Verify(c => c.SetCacheData(It.IsAny<string>(), templates, true), Times.Once);
    }

    [Test]
    public async Task GetNotificationTemplateListAsync_LogsError_OnException()
    {
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Throws(new Exception("Test exception"));

        var result = await _service.GetNotificationTemplateListAsync();

        Assert.That(result, Is.Null);
        _loggerMock.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("GetNotificationTemplateListAsync")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Test]
    public async Task SendNotificationByTemplateAsync_ReturnsNull_WhenTemplateNotFound()
    {
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Returns(new List<NotificationTemplateDTO>());

        var result = await _service.SendNotificationByTemplateAsync(1, NotificationTypeEnum.NotificationType_Email, 99, new List<KeyValuePair<string, string>>(), new List<string>());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task SendNotificationByTemplateAsync_CallsSendNotificationAsync_WhenTemplateFound()
    {
        var template = new NotificationTemplateDTO
        {
            Id = 1,
            NotificationTemplateGroupId = 2,
            IsHtml = true,
            SubjectTemplateText = "Subject",
            MessageTemplateText = "Message"
        };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Returns(new List<NotificationTemplateDTO> { template });
        _cultureServiceMock.Setup(c => c.UtcNow()).Returns(DateTimeOffset.UtcNow);
        _socialNetworksRepositoryMock.Setup(r => r.CreateNewNotificationsync(It.IsAny<NotificationDTO>(), null))
            .ReturnsAsync(new NotificationDTO { Id = 123 });

        var result = await _service.SendNotificationByTemplateAsync(
            1,
            NotificationTypeEnum.NotificationType_Email,
            1,
            new List<KeyValuePair<string, string>>(),
            new List<string> { "to@test.com" }
        );

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(123));
    }

    [Test]
    public void SendNotificationByTemplateAsync_LogsErrorAndThrows_OnException()
    {
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<NotificationTemplateDTO>>(It.IsAny<string>()))
            .Throws(new Exception("Test exception"));

        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.SendNotificationByTemplateAsync(1, NotificationTypeEnum.NotificationType_Email, 1, new List<KeyValuePair<string, string>>(), new List<string>());
        });
        _loggerMock.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("SendNotificationByTemplateAsync")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Test]
    public async Task SendNotificationAsync_SetsSenderAndSavesNotification()
    {
        var notification = new NotificationDTO
        {
            NotificationTypeFk = (long)NotificationTypeEnum.NotificationType_Email,
            Receiver = new List<string> { "to@test.com" }
        };
        _cultureServiceMock.Setup(c => c.UtcNow()).Returns(DateTimeOffset.UtcNow);
        _socialNetworksRepositoryMock.Setup(r => r.CreateNewNotificationsync(It.IsAny<NotificationDTO>(), null))
            .ReturnsAsync((NotificationDTO n, object _) => n);

        var result = await _service.SendNotificationAsync(notification);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Sender, Is.EqualTo(_emailSettingsOptions.SenderEmail));
    }

    [Test]
    public void SendNotificationAsync_LogsErrorAndThrows_OnException()
    {
        var notification = new NotificationDTO
        {
            NotificationTypeFk = (long)NotificationTypeEnum.NotificationType_Email,
            Receiver = new List<string> { "to@test.com" }
        };
        _socialNetworksRepositoryMock.Setup(r => r.CreateNewNotificationsync(It.IsAny<NotificationDTO>(), null))
            .Throws(new Exception("Test exception"));

        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.SendNotificationAsync(notification);
        });
        _loggerMock.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("SendNotificationAsync")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }
}