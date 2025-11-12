using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;

namespace Vito.Transverse.Identity.UnitTests.BAL;

[TestFixture]
public class CachingServiceMemoryCacheTest
{
    private readonly Mock<IMemoryCache> _memoryCacheMock;
    private readonly Mock<ICultureRepository> _cultureRepositoryMock;
    private readonly Mock<IOptions<MemoryCacheSettingsOptions>> _optionsMock;
    private readonly Mock<ILogger<CachingServiceMemoryCache>> _loggerMock;
    private readonly MemoryCacheSettingsOptions _settings;
    private readonly CachingServiceMemoryCache _service;

    public CachingServiceMemoryCacheTest()
    {
        _memoryCacheMock = new Mock<IMemoryCache>();
        _cultureRepositoryMock = new Mock<ICultureRepository>();
        _loggerMock = new Mock<ILogger<CachingServiceMemoryCache>>();
        _settings = new MemoryCacheSettingsOptions { IsCacheEnabled = true, CacheExpirationInMinutes = 10 };
        _optionsMock = new Mock<IOptions<MemoryCacheSettingsOptions>>();
        _optionsMock.Setup(x => x.Value).Returns(_settings);

        _service = new CachingServiceMemoryCache(
            _memoryCacheMock.Object,
            _cultureRepositoryMock.Object,
            _optionsMock.Object,
            _loggerMock.Object
        );
    }

    [Test]
    public void CacheDataExistsByKey_ReturnsTrue_WhenKeyExists()
    {
        object value = "test";
        _memoryCacheMock.Setup(m => m.TryGetValue("key", out value)).Returns(true);

        var result = _service.CacheDataExistsByKey("key");

        Assert.That(result, Is.True);
    }

    [Test]
    public void CacheDataExistsByKey_ReturnsFalse_WhenKeyDoesNotExist()
    {
        object value = null;
        _memoryCacheMock.Setup(m => m.TryGetValue("key", out value)).Returns(false);

        var result = _service.CacheDataExistsByKey("key");

        Assert.That(result, Is.False);
    }

    [Test]
    public void CacheDataExistsByKey_LogsError_OnException()
    {
        _memoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out It.Ref<object>.IsAny))
            .Throws(new Exception("fail"));

        var result = _service.CacheDataExistsByKey("key");

        Assert.That(result, Is.False);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "CacheDataExistsByKey"), Times.Once);
    }

    [Test]
    public void DeleteCacheDataByKey_RemovesItem_AndReturnsTrue()
    {
        object value = "exists";
        _memoryCacheMock.Setup(m => m.TryGetValue("key", out value)).Returns(true);
        _memoryCacheMock.Setup(m => m.Remove("key"));

        var result = _service.DeleteCacheDataByKey("key");

        Assert.That(result, Is.True);
        _memoryCacheMock.Verify(m => m.Remove("key"), Times.Once);
    }

    [Test]
    public void DeleteCacheDataByKey_ReturnsFalse_WhenKeyNotFound()
    {
        object value = null;
        _memoryCacheMock.Setup(m => m.TryGetValue("key", out value)).Returns(false);

        var result = _service.DeleteCacheDataByKey("key");

        Assert.That(result, Is.False);
        _memoryCacheMock.Verify(m => m.Remove(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void DeleteCacheDataByKey_LogsError_OnException()
    {
        _memoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out It.Ref<object>.IsAny))
            .Throws(new Exception("fail"));

        var result = _service.DeleteCacheDataByKey("key");

        Assert.That(result, Is.False);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "DeleteCacheDataByKey"), Times.Once);
    }

    [Test]
    public void ClearCacheData_CallsClear_WhenEnabled()
    {
        var concreteMemoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new CachingServiceMemoryCache(
            concreteMemoryCache,
            _cultureRepositoryMock.Object,
            _optionsMock.Object,
            _loggerMock.Object
        );
        concreteMemoryCache.Set("a", "b");

        var result = service.ClearCacheData();

        Assert.That(result, Is.True);
        Assert.That(concreteMemoryCache.TryGetValue("a", out _), Is.False);
    }

    [Test]
    public void ClearCacheData_LogsError_OnException()
    {
        _memoryCacheMock.As<IDisposable>().Setup(m => m.Dispose()).Throws(new Exception("fail"));

        var result = _service.ClearCacheData();

        Assert.That(result, Is.False);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "DeleteCacheDataByKey"), Times.Once);
    }

    [Test]
    public void GetCacheDataByKey_ReturnsDefault_WhenNotFound()
    {
        _memoryCacheMock.Setup(m => m.Get("key")).Returns(null);

        var result = _service.GetCacheDataByKey<List<string>>("key");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetCacheDataByKey_LogsError_OnException()
    {
        _memoryCacheMock.Setup(m => m.Get(It.IsAny<string>())).Throws(new Exception("fail"));

        var result = _service.GetCacheDataByKey<List<string>>("key");

        Assert.That(result, Is.Null);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "GetCacheDataByKey"), Times.Once);
    }

    [Test]
    public void SetCacheData_SetsValue_AndReturnsTrue()
    {
        _cultureRepositoryMock.Setup(c => c.UtcNow()).Returns(DateTimeOffset.UtcNow);
        _memoryCacheMock.Setup(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTimeOffset>())).Returns("ok");

        var result = _service.SetCacheData("key", new { Name = "test" });

        Assert.That(result, Is.True);
    }

    [Test]
    public void SetCacheData_LogsError_OnException()
    {
        _cultureRepositoryMock.Setup(c => c.UtcNow()).Returns(DateTimeOffset.UtcNow);
        _memoryCacheMock.Setup(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTimeOffset>())).Throws(new Exception("fail"));

        var result = _service.SetCacheData("key", new { Name = "test" });

        Assert.That(result, Is.False);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "SetCacheData"), Times.Once);
    }

    [Test]
    public void RefreshCacheDataByKey_ThrowsNotImplementedException()
    {
        Assert.That(() => _service.RefreshCacheDataByKey("key"), Throws.TypeOf<NotImplementedException>());
    }
}