namespace Vito.Transverse.Identity.UnitTests.DAL;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;
using Vito.Transverse.Identity.UnitTests.Helpers;

[TestFixture]
public class AuditRepositoryTest
{
    private Mock<ILogger<AuditRepository>> _loggerMock = new();
    private Mock<IOptions<DataBaseSettingsOptions>> _optionsMock = new();
    private IDataBaseContextFactory _dbContextFactoryMock;
    private AuditRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _optionsMock.Setup(x => x.Value).Returns(new DataBaseSettingsOptions
        {
            ConnectionStrings = [
            
                new()
                {
                    ConnectionName = "TransverseDB",
                    ConnectionType =  Framework.Common.Enums.ConnectionStringTypeEnum.SQLServer,
                    ConnectionString = "FakeConnectionString",
                    MaxRetryDelay = 1,
                    RetryCount = 1,
                    TimeOut = 1
                }
            ]
        });
        _repository = new AuditRepository(_dbContextFactoryMock, _optionsMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task AddNewAuditRecord_ShouldAddRecord()
    {
        var dto = new AuditRecordDTO { AuditTypeFk = 1, AuditEntityIndex = "idx", AuditChanges = "changes" };
        var result = await _repository.AddNewAuditRecord(dto);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AuditTypeFk, Is.EqualTo(dto.AuditTypeFk));
    }

    [Test]
    public async Task GetEntitiesListAsync_ShouldReturnEntities()
    {
        Expression<Func<Entity, bool>> filter = x => true;
        var result = await _repository.GetEntitiesListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<EntityDTO>>());
    }

    [Test]
    public async Task GetCompanyEntityAuditsListAsync_ShouldReturnAudits()
    {
        Expression<Func<CompanyEntityAudit, bool>> filter = x => true;
        var result = await _repository.GetCompanyEntityAuditsListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<CompanyEntityAuditDTO>>());
    }

    [Test]
    public async Task GetAuditRecordListAsync_ShouldReturnAuditRecords()
    {
        Expression<Func<AuditRecord, bool>> filter = x => true;
        var result = await _repository.GetAuditRecordListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<AuditRecordDTO>>());
    }

    [Test]
    public async Task GetActivityLogListAsync_ShouldReturnActivityLogs()
    {
        Expression<Func<ActivityLog, bool>> filter = x => true;
        var result = await _repository.GetActivityLogListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<ActivityLogDTO>>());
    }

    [Test]
    public async Task GetNotificationsListAsync_ShouldReturnNotifications()
    {
        Expression<Func<Notification, bool>> filter = x => true;
        var result = await _repository.GetNotificationsListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<NotificationDTO>>());
    }

    [Test]
    public async Task GetDatabaseHealth_ShouldReturnHealthData()
    {
        var result = await _repository.GetDatabaseHealth();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<Dictionary<string, object>>());
        Assert.That(result.ContainsKey("ConnectionType"), Is.True);
    }
}