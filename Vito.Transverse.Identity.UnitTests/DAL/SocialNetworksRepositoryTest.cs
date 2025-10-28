using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.UnitTests.Helpers;

namespace Vito.Transverse.Identity.UnitTests.DAL;

[TestFixture]
public class SocialNetworksRepositoryTest
{
    private IDataBaseContextFactory _dbContextFactoryMock;
    private Mock<ILogger<SocialNetworksRepository>> _loggerMock = new();
    private SocialNetworksRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _repository = new SocialNetworksRepository(_dbContextFactoryMock, _loggerMock.Object);
    }

    [Test]
    public async Task GetNotificationTemplateListAsync_ReturnsList()
    {
        Expression<Func<NotificationTemplate, bool>> filter = x => true;
        var result = await _repository.GetNotificationTemplateListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<NotificationTemplateDTO>>());
    }

    [Test]
    public async Task CreateNewNotificationsync_ReturnsNotificationDTO()
    {
        var dto = new NotificationDTO
        {
            Id = 1,
            NotificationTypeFk = 1,
            CompanyFk = 1,
            NotificationTemplateGroupFk = 1,
            CreationDate = DateTime.UtcNow
        };
        var result = await _repository.CreateNewNotificationsync(dto);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(dto.Id));
    }
}