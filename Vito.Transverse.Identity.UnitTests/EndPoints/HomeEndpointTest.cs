using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Endpoints;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Application.TransverseServices.Security;

namespace Vito.Transverse.Identity.UnitTests.EdnPoints;

[TestFixture]
public class HomeEndpointTest
{
    private Mock<ICultureService> _cultureServiceMock;
    private Mock<ISecurityService> _securityServiceMock;
    private DefaultHttpContext _httpContext;
    private HttpRequest _httpRequest;

    [SetUp]
    public void SetUp()
    {
        _cultureServiceMock = new Mock<ICultureService>();
        _securityServiceMock = new Mock<ISecurityService>();
        _httpContext = new DefaultHttpContext();
        _httpRequest = _httpContext.Request;
    }

    [Test]
    public async Task DetectV0_9Aync_ReturnsOkWithExpectedMessage()
    {
        var now = DateTime.UtcNow;
        _cultureServiceMock.Setup(x => x.UtcNow()).Returns(new DateTimeOffset(now));
        var deviceInfo = new DeviceInformationDTO();
        _httpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] = deviceInfo;

        var result = await HealthEndPoint.DetectV0_9Aync(_httpRequest, _cultureServiceMock.Object);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value.PingMessage, Is.EqualTo("Detect v0.9"));
        Assert.That(result.Value.DeviceInformation, Is.EqualTo(deviceInfo));
    }

    [Test]
    public async Task DetectAync_ReturnsOkWithExpectedMessage()
    {
        var now = DateTime.UtcNow;
        _cultureServiceMock.Setup(x => x.UtcNow()).Returns(new DateTimeOffset(now));
        var deviceInfo = new DeviceInformationDTO();
        _httpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] = deviceInfo;

        var result = await HealthEndPoint.DetectAync(_httpRequest, _securityServiceMock.Object, _cultureServiceMock.Object);

        Assert.That(result.Result, Is.Not.Null);
        Assert.That(result.Result is Microsoft.AspNetCore.Http.HttpResults.Ok<Vito.Framework.Common.DTO.PingResponseDTO>, Is.True);
        var okResult = result.Result as Microsoft.AspNetCore.Http.HttpResults.Ok<Vito.Framework.Common.DTO.PingResponseDTO>;
        Assert.That(okResult.Value.PingMessage, Is.EqualTo("Detect v1.0"));
        Assert.That(okResult.Value.DeviceInformation, Is.EqualTo(deviceInfo));
    }

    [Test]
    public async Task HomePingV0_9Aync_ReturnsOkWithExpectedMessage()
    {
        var now = DateTime.UtcNow;
        _cultureServiceMock.Setup(x => x.UtcNow()).Returns(new DateTimeOffset(now));

        var result = await HealthEndPoint.HomePingV0_9Aync(_httpRequest, _cultureServiceMock.Object);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value.PingMessage, Is.EqualTo("Pong v0.9"));
    }

    [Test]
    public async Task HomePingAync_ReturnsOkWithExpectedMessage()
    {
        var now = DateTime.UtcNow;
        _cultureServiceMock.Setup(x => x.UtcNow()).Returns(new DateTimeOffset(now));

        var result = await HealthEndPoint.HomePingAync(_httpRequest, _securityServiceMock.Object, _cultureServiceMock.Object);

        Assert.That(result.Result, Is.Not.Null);
        Assert.That(result.Result is Microsoft.AspNetCore.Http.HttpResults.Ok<Vito.Framework.Common.DTO.PingResponseDTO>, Is.True);
        var okResult = result.Result as Microsoft.AspNetCore.Http.HttpResults.Ok<Vito.Framework.Common.DTO.PingResponseDTO>;
        Assert.That(okResult.Value.PingMessage, Is.EqualTo("Pong v1.0"));
    }
}