using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Entities.Enums;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.UnitTests.BAL;

public class AuditServiceTest
{
    private Mock<IAuditRepository> _auditRepositoryMock;
    private Mock<ILogger<AuditService>> _loggerMock;
    private Mock<ICultureService> _cultureRepositoryMock;
    private Mock<ICachingServiceMemoryCache> _cachingServiceMock;
    private AuditService _service;

    [SetUp]
    public void SetUp()
    {
        _auditRepositoryMock = new Mock<IAuditRepository>();
        _loggerMock = new Mock<ILogger<AuditService>>();
        _cultureRepositoryMock = new Mock<ICultureService>();
        _cachingServiceMock = new Mock<ICachingServiceMemoryCache>();
        _service = new AuditService(
            _loggerMock.Object,
            _auditRepositoryMock.Object,
            _cultureRepositoryMock.Object,
            _cachingServiceMock.Object
        );
    }

    [Test]
    public async Task DeleteRowAuditAsync_ReturnsNull_WhenAuditNotEnabled()
    {
        _auditRepositoryMock.Setup(r => r.GetCompanyEntityAuditsListAsync(It.IsAny<Expression<Func<CompanyEntityAudit, bool>>>(), null))
            .ReturnsAsync(new List<CompanyEntityAuditDTO>());
        var result = await _service.DeleteRowAuditAsync(1, 2, new { Id = 1 }, "1", new DeviceInformationDTO());
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task NewRowAuditAsync_ReturnsNull_WhenAuditNotEnabled()
    {
        _auditRepositoryMock.Setup(r => r.GetCompanyEntityAuditsListAsync(It.IsAny<Expression<Func<CompanyEntityAudit, bool>>>(), null))
            .ReturnsAsync(new List<CompanyEntityAuditDTO>());
        var result = await _service.NewRowAuditAsync(1, 2, new { Id = 1 }, "1", new DeviceInformationDTO());
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task UpdateRowAuditAsync_ReturnsNull_WhenAuditNotEnabled()
    {
        _auditRepositoryMock.Setup(r => r.GetCompanyEntityAuditsListAsync(It.IsAny<Expression<Func<CompanyEntityAudit, bool>>>(), null))
            .ReturnsAsync(new List<CompanyEntityAuditDTO>());
        var result = await _service.UpdateRowAuditAsync(1, 2, new { Id = 1 }, new { Id = 2 }, "1", new DeviceInformationDTO());
        Assert.That(result, Is.Null);
    }

    [Test]
    public void IsCompanyEntityEnableForAuditAsync_ThrowsException_AndLogsError()
    {
        _auditRepositoryMock.Setup(r => r.GetCompanyEntityAuditsListAsync(It.IsAny<Expression<Func<CompanyEntityAudit, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.IsCompanyEntityEnableForAuditAsync(1, new { Id = 1 }, EntityAuditTypeEnum.EntityAuditType_AddRow);
        });
    }

    [Test]
    public void GetEntityListAsync_ThrowsException_AndLogsError()
    {
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<EntityDTO>>(It.IsAny<string>()))
            .Returns((List<EntityDTO>)null);
        _auditRepositoryMock.Setup(r => r.GetEntitiesListAsync(It.IsAny<Expression<Func<Entity, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetEntityListAsync();
        });
    }

    [Test]
    public void GetCompanyEntityAuditsListAsync_ThrowsException_AndLogsError()
    {
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CompanyEntityAuditDTO>>(It.IsAny<string>()))
            .Returns((List<CompanyEntityAuditDTO>)null);
        _auditRepositoryMock.Setup(r => r.GetCompanyEntityAuditsListAsync(It.IsAny<Expression<Func<CompanyEntityAudit, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetCompanyEntityAuditsListAsync(1);
        });
    }

    [Test]
    public void GetAuditRecordsListAsync_ThrowsException_AndLogsError()
    {
        _auditRepositoryMock.Setup(r => r.GetAuditRecordListAsync(It.IsAny<Expression<Func<AuditRecord, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetAuditRecordsListAsync(1);
        });
    }

    [Test]
    public void GetActivityLogListAsync_ThrowsException_AndLogsError()
    {
        _auditRepositoryMock.Setup(r => r.GetActivityLogListAsync(It.IsAny<Expression<Func<ActivityLog, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetActivityLogListAsync(1);
        });
    }

    [Test]
    public void GetNotificationsListAsync_ThrowsException_AndLogsError()
    {
        _auditRepositoryMock.Setup(r => r.GetNotificationsListAsync(It.IsAny<Expression<Func<Notification, bool>>>(), null))
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetNotificationsListAsync(1);
        });
    }

    [Test]
    public void GetDatabaseHealth_ThrowsException_AndLogsError()
    {
        _auditRepositoryMock.Setup(r => r.GetDatabaseHealth())
            .ThrowsAsync(new Exception("Test exception"));
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _service.GetDatabaseHealth();
        });
    }
}