using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Media;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;
using Vito.Transverse.Identity.Entities.Enums;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.UnitTests.BAL;

[TestFixture]
public class MediaServiceTest
{
    private readonly Mock<IMediaRepository> _mediaRepositoryMock;
    private readonly Mock<ICachingServiceMemoryCache> _cachingServiceMock;
    private readonly Mock<ICultureRepository> _cultureRepositoryMock;
    private readonly Mock<ILogger<MediaService>> _loggerMock;
    private readonly MediaService _service;

    public MediaServiceTest()
    {
        _mediaRepositoryMock = new Mock<IMediaRepository>();
        _cachingServiceMock = new Mock<ICachingServiceMemoryCache>();
        _cultureRepositoryMock = new Mock<ICultureRepository>();
        _loggerMock = new Mock<ILogger<MediaService>>();

        _service = new MediaService(
            _mediaRepositoryMock.Object,
            _cachingServiceMock.Object,
            _cultureRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Test]
    public async Task GetPictureByName_ReturnsPicture_WhenFound()
    {
        var companyId = 1L;
        var name = "pic1";
        var list = new List<PictureDTO> { new PictureDTO { Name = name } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Returns(list);

        var result = await _service.GetPictureByName(companyId, name);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(name));
    }

    [Test]
    public async Task GetPictureByName_ReturnsNull_WhenNotFound()
    {
        var companyId = 1L;
        var name = "pic1";
        var list = new List<PictureDTO> { new PictureDTO { Name = "other" } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Returns(list);

        var result = await _service.GetPictureByName(companyId, name);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPictureByName_LogsError_OnException()
    {
        var companyId = 1L;
        var name = "pic1";
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Throws(new Exception("fail"));

        Assert.That(async () => await _service.GetPictureByName(companyId, name), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), nameof(_service.GetPictureByName)), Times.Once);
    }

    [Test]
    public async Task GetPictureByNameWildCard_ReturnsMatchingPictures()
    {
        var companyId = 1L;
        var wildCard = "pic";
        var list = new List<PictureDTO>
        {
            new PictureDTO { Name = "pic1" },
            new PictureDTO { Name = "other" },
            new PictureDTO { Name = "Pic2" }
        };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Returns(list);

        var result = await _service.GetPictureByNameWildCard(companyId, wildCard);

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(x => x.Name == "pic1"), Is.True);
        Assert.That(result.Any(x => x.Name == "Pic2"), Is.True);
    }

    [Test]
    public void GetPictureByNameWildCard_LogsError_OnException()
    {
        var companyId = 1L;
        var wildCard = "pic";
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Throws(new Exception("fail"));

        Assert.That(async () => await _service.GetPictureByNameWildCard(companyId, wildCard), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), nameof(_service.GetPictureByName)), Times.Once);
    }

    [Test]
    public async Task GetPictureList_ReturnsFromCache_WhenExists()
    {
        var companyId = 1L;
        var cacheKey = CacheItemKeysEnum.PictureListByCompanyId + companyId.ToString();
        var cachedList = new List<PictureDTO> { new PictureDTO { Name = "pic1" } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(cacheKey)).Returns(cachedList);

        var result = await _service.GetPictureList(companyId);

        Assert.That(result, Is.EqualTo(cachedList));
        _cachingServiceMock.Verify(c => c.GetCacheDataByKey<List<PictureDTO>>(cacheKey), Times.Once);
        _mediaRepositoryMock.Verify(r => r.GetPictureList(It.IsAny<System.Linq.Expressions.Expression<Func<Picture, bool>>>(), null), Times.Never);
    }

    [Test]
    public async Task GetPictureList_FetchesAndCaches_WhenCacheIsNull()
    {
        var companyId = 1L;
        var cacheKey = CacheItemKeysEnum.PictureListByCompanyId + companyId.ToString();
        var repoList = new List<PictureDTO> { new PictureDTO { Name = "pic1" } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(cacheKey)).Returns((List<PictureDTO>)null);
        _mediaRepositoryMock.Setup(r => r.GetPictureList(It.IsAny<System.Linq.Expressions.Expression<Func<Picture, bool>>>(), null)).ReturnsAsync(repoList);
        _cachingServiceMock.Setup(c => c.SetCacheData(cacheKey, repoList, true)).Returns(true);

        var result = await _service.GetPictureList(companyId);

        Assert.That(result, Is.EqualTo(repoList));
        _cachingServiceMock.Verify(c => c.SetCacheData(cacheKey, repoList, true), Times.Once);
    }

    [Test]
    public void GetPictureList_LogsError_OnException()
    {
        var companyId = 1L;
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<PictureDTO>>(It.IsAny<string>())).Throws(new Exception("fail"));

        Assert.That(async () => await _service.GetPictureList(companyId), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), nameof(_service.GetPictureByName)), Times.Once);
    }

    [Test]
    public async Task AddNewPictureAsync_ReturnsTrue_WhenSuccess()
    {
        var picture = new PictureDTO { Name = "LaPic" };
        var now = DateTimeOffset.UtcNow;
        _cultureRepositoryMock.Setup(c => c.UtcNow()).Returns(now);
        _mediaRepositoryMock.Setup(r => r.AddNewPictureAsync(It.IsAny<PictureDTO>(), null)).ReturnsAsync(true);

        var result = await _service.AddNewPictureAsync(picture);

        Assert.That(result, Is.True);
    }

    [Test]
    public void AddNewPictureAsync_LogsError_OnException()
    {
        var picture = new PictureDTO { Name = "LaPic" };
        _mediaRepositoryMock.Setup(r => r.AddNewPictureAsync(It.IsAny<PictureDTO>(), null)).Throws(new Exception("fail"));

        Assert.That(async () => await _service.AddNewPictureAsync(picture), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), nameof(_service.GetPictureList)), Times.Once);
    }
}