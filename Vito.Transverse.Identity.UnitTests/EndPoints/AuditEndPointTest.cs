using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Presentation.Api.Endpoints;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.UnitTests.EdnPoints;

[TestFixture]
public class AuditEndPointTest
{
    private Mock<IAuditService> _auditServiceMock;
    private DefaultHttpContext _httpContext;
    private HttpRequest _httpRequest;

    [SetUp]
    public void SetUp()
    {
        _auditServiceMock = new Mock<IAuditService>();
        _httpContext = new DefaultHttpContext();
        _httpRequest = _httpContext.Request;
    }

    [Test]
    public async Task GetCompanyEntityAuditsListAsync_ReturnsOk_WhenListIsNotNull()
    {
        var list = new List<CompanyEntityAuditDTO> { new CompanyEntityAuditDTO() };
        _auditServiceMock.Setup(x => x.GetCompanyEntityAuditsListAsync(It.IsAny<long?>()))
            .ReturnsAsync(list);

        var result = await AuditEndPoint.GetCompanyEntityAuditsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<Ok<List<CompanyEntityAuditDTO>>>());
        var ok = result.Result as Ok<List<CompanyEntityAuditDTO>>;
        Assert.That(ok.Value, Is.EqualTo(list));
    }

    [Test]
    public async Task GetCompanyEntityAuditsListAsync_ReturnsNotFound_WhenListIsNull()
    {
        _auditServiceMock.Setup(x => x.GetCompanyEntityAuditsListAsync(It.IsAny<long?>()))
            .ReturnsAsync((List<CompanyEntityAuditDTO>)null);

        var result = await AuditEndPoint.GetCompanyEntityAuditsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<NotFound>());
    }

    [Test]
    public async Task GetAuditRecordsListAsync_ReturnsOk_WhenListIsNotNull()
    {
        var list = new List<AuditRecordDTO> { new AuditRecordDTO() };
        _auditServiceMock.Setup(x => x.GetAuditRecordsListAsync(It.IsAny<long?>()))
            .ReturnsAsync(list);

        var result = await AuditEndPoint.GetAuditRecordsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<Ok<List<AuditRecordDTO>>>());
        var ok = result.Result as Ok<List<AuditRecordDTO>>;
        Assert.That(ok.Value, Is.EqualTo(list));
    }

    [Test]
    public async Task GetAuditRecordsListAsync_ReturnsNotFound_WhenListIsNull()
    {
        _auditServiceMock.Setup(x => x.GetAuditRecordsListAsync(It.IsAny<long?>()))
            .ReturnsAsync((List<AuditRecordDTO>)null);

        var result = await AuditEndPoint.GetAuditRecordsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<NotFound>());
    }

    [Test]
    public async Task GetActivityLogListAsync_ReturnsOk_WhenListIsNotNull()
    {
        var list = new List<ActivityLogDTO> { new ActivityLogDTO() };
        _auditServiceMock.Setup(x => x.GetActivityLogListAsync(It.IsAny<long?>()))
            .ReturnsAsync(list);

        var result = await AuditEndPoint.GetActivityLogListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<Ok<List<ActivityLogDTO>>>());
        var ok = result.Result as Ok<List<ActivityLogDTO>>;
        Assert.That(ok.Value, Is.EqualTo(list));
    }

    [Test]
    public async Task GetActivityLogListAsync_ReturnsNotFound_WhenListIsNull()
    {
        _auditServiceMock.Setup(x => x.GetActivityLogListAsync(It.IsAny<long?>()))
            .ReturnsAsync((List<ActivityLogDTO>)null);

        var result = await AuditEndPoint.GetActivityLogListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<NotFound>());
    }

    [Test]
    public async Task GetNotificationsListAsync_ReturnsOk_WhenListIsNotNull()
    {
        var list = new List<NotificationDTO> { new NotificationDTO() };
        _auditServiceMock.Setup(x => x.GetNotificationsListAsync(It.IsAny<long?>()))
            .ReturnsAsync(list);

        var result = await AuditEndPoint.GetNotificationsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<Ok<List<NotificationDTO>>>());
        var ok = result.Result as Ok<List<NotificationDTO>>;
        Assert.That(ok.Value, Is.EqualTo(list));
    }

    [Test]
    public async Task GetNotificationsListAsync_ReturnsNotFound_WhenListIsNull()
    {
        _auditServiceMock.Setup(x => x.GetNotificationsListAsync(It.IsAny<long?>()))
            .ReturnsAsync((List<NotificationDTO>)null);

        var result = await AuditEndPoint.GetNotificationsListAsync(_httpRequest, _auditServiceMock.Object, 1);

        Assert.That(result.Result, Is.TypeOf<NotFound>());
    }

    [Test]
    public async Task GetEntityListAsync_ReturnsOk_WhenListIsNotNull()
    {
        var list = new List<EntityDTO> { new EntityDTO() };
        _auditServiceMock.Setup(x => x.GetEntityListAsync())
            .ReturnsAsync(list);

        var result = await AuditEndPoint.GetEntityListAsync(_httpRequest, _auditServiceMock.Object);

        Assert.That(result.Result, Is.TypeOf<Ok<List<EntityDTO>>>());
        var ok = result.Result as Ok<List<EntityDTO>>;
        Assert.That(ok.Value, Is.EqualTo(list));
    }

    [Test]
    public async Task GetEntityListAsync_ReturnsNotFound_WhenListIsNull()
    {
        _auditServiceMock.Setup(x => x.GetEntityListAsync())
            .ReturnsAsync((List<EntityDTO>)null);

        var result = await AuditEndPoint.GetEntityListAsync(_httpRequest, _auditServiceMock.Object);

        Assert.That(result.Result, Is.TypeOf<NotFound>());
    }
}