using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Localization;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization;
using Vito.Transverse.Identity.Entities.Enums;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.UnitTests.BAL;

[TestFixture]
public class LocalizationServiceTest
{
    private readonly Mock<ILocalizationRepository> _localizationRepositoryMock;
    private readonly Mock<ICachingServiceMemoryCache> _cachingServiceMock;
    private readonly Mock<ILogger<LocalizationService>> _loggerMock;
    private readonly Mock<IOptions<CultureSettingsOptions>> _optionsMock;
    private readonly CultureSettingsOptions _settings;
    private readonly LocalizationService _service;

    public LocalizationServiceTest()
    {
        _localizationRepositoryMock = new Mock<ILocalizationRepository>();
        _cachingServiceMock = new Mock<ICachingServiceMemoryCache>();
        _loggerMock = new Mock<ILogger<LocalizationService>>();
        _settings = new CultureSettingsOptions { LocalizationJsonFilePath = "test_{0}.json", AutoAddMissingTranslations = true };
        _optionsMock = new Mock<IOptions<CultureSettingsOptions>>();
        _optionsMock.Setup(x => x.Value).Returns(_settings);

        _service = new LocalizationService(
            _localizationRepositoryMock.Object,
            _cachingServiceMock.Object,
            _loggerMock.Object,
            _optionsMock.Object
        );
    }

    [Test]
    public async Task GetLocalizedMessagesListByApplicationAsync_ReturnsFromCache()
    {
        var applicationId = 1L;
        var cacheKey = CacheItemKeysEnum.CultureTranslationsListByApplicationId + applicationId.ToString();
        var cachedList = new List<CultureTranslationDTO> { new CultureTranslationDTO { ApplicationFk = applicationId } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey)).Returns(cachedList);

        var result = await _service.GetLocalizedMessagesListByApplicationAsync(applicationId);

        Assert.That(result, Is.EqualTo(cachedList));
        _cachingServiceMock.Verify(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey), Times.Once);
        _localizationRepositoryMock.Verify(r => r.GetLocalizedMessagesListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<CultureTranslation, bool>>>(), null), Times.Never);
    }

    [Test]
    public async Task GetLocalizedMessagesListByApplicationAsync_FetchesAndCaches_WhenCacheIsNull()
    {
        var applicationId = 1L;
        var cacheKey = CacheItemKeysEnum.CultureTranslationsListByApplicationId + applicationId.ToString();
        var repoList = new List<CultureTranslationDTO> { new CultureTranslationDTO { ApplicationFk = applicationId } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey)).Returns((List<CultureTranslationDTO>)null);
        _localizationRepositoryMock.Setup(r => r.GetLocalizedMessagesListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<CultureTranslation, bool>>>(), null)).ReturnsAsync(repoList);
        _cachingServiceMock.Setup(c => c.SetCacheData(cacheKey, repoList, true)).Returns(true);

        var result = await _service.GetLocalizedMessagesListByApplicationAsync(applicationId);

        Assert.That(result, Is.EqualTo(repoList));
        _cachingServiceMock.Verify(c => c.SetCacheData(cacheKey, repoList, true), Times.Once);
    }

    [Test]
    public async Task GetLocalizedMessagesListByApplicationAndCultureAsync_ReturnsFromCache()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var cacheKey = CacheItemKeysEnum.CultureTranslationsListByApplicationIdCultureId + applicationId.ToString() + cultureId;
        var cachedList = new List<CultureTranslationDTO> { new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = cultureId } };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey)).Returns(cachedList);

        var result = await _service.GetLocalizedMessagesListByApplicationAndCultureAsync(applicationId, cultureId);

        Assert.That(result, Is.EqualTo(cachedList));
        _cachingServiceMock.Verify(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey), Times.Once);
    }

    [Test]
    public async Task GetLocalizedMessagesListByApplicationAndCultureAsync_FetchesAndCaches_WhenCacheIsNull()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var cacheKey = CacheItemKeysEnum.CultureTranslationsListByApplicationIdCultureId + applicationId.ToString() + cultureId;
        var allList = new List<CultureTranslationDTO>
        {
            new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = cultureId, TranslationKey = "key1", TranslationValue = "val1" },
            new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = "fr-FR", TranslationKey = "key2", TranslationValue = "val2" }
        };
        var filteredList = allList.Where(x => x.CultureFk == cultureId).ToList();

        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(cacheKey)).Returns((List<CultureTranslationDTO>)null);
        _localizationRepositoryMock.Setup(r => r.GetLocalizedMessagesListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<CultureTranslation, bool>>>(), null)).ReturnsAsync(allList);
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.Is<string>(k => k.StartsWith(CacheItemKeysEnum.CultureTranslationsListByApplicationId.ToString())))).Returns((List<CultureTranslationDTO>)null);
        _cachingServiceMock.Setup(c => c.SetCacheData(cacheKey, It.IsAny<List<CultureTranslationDTO>>(), true)).Returns(true);
        _cachingServiceMock.Setup(c => c.SetCacheData(It.Is<string>(k => k.StartsWith(CacheItemKeysEnum.CultureTranslationsListByApplicationId.ToString())), It.IsAny<List<CultureTranslationDTO>>(), true)).Returns(true);

        // Patch file system for DEBUG block (simulate no exception)
        File.Delete(_settings.LocalizationJsonFilePath.Replace("{0}", cultureId));
        File.WriteAllText(_settings.LocalizationJsonFilePath.Replace("{0}", cultureId), "{}");

        var result = await _service.GetLocalizedMessagesListByApplicationAndCultureAsync(applicationId, cultureId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.All(x => x.CultureFk == cultureId), Is.True);
        _cachingServiceMock.Verify(c => c.SetCacheData(cacheKey, It.IsAny<List<CultureTranslationDTO>>(), true), Times.Once);
    }

    [Test]
    public void GetLocalizedMessagesListByApplicationAndCultureAsync_LogsError_OnException()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.IsAny<string>())).Throws(new Exception("fail"));

        Assert.That(async () => await _service.GetLocalizedMessagesListByApplicationAndCultureAsync(applicationId, cultureId), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "GetLocalizedMessagesListByApplicationAndCultureAsync"), Times.Once);
    }

    [Test]
    public async Task GetLocalizedMessagesListByKeyAsync_ReturnsFilteredList()
    {
        var applicationId = 1L;
        var key = "key1";
        var allList = new List<CultureTranslationDTO>
        {
            new CultureTranslationDTO { ApplicationFk = applicationId, TranslationKey = "key1", TranslationValue = "val1" },
            new CultureTranslationDTO { ApplicationFk = applicationId, TranslationKey = "key2", TranslationValue = "val2" }
        };
        _localizationRepositoryMock.Setup(r => r.GetLocalizedMessagesListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<CultureTranslation, bool>>>(), null)).ReturnsAsync(allList);
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.IsAny<string>())).Returns(allList);

        var result = await _service.GetLocalizedMessagesListByKeyAsync(applicationId, key);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.All(x => x.TranslationKey == key), Is.True);
    }

    [Test]
    public void GetLocalizedMessagesListByKeyAsync_LogsError_OnException()
    {
        var applicationId = 1L;
        var key = "key1";
        _localizationRepositoryMock.Setup(r => r.GetLocalizedMessagesListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<CultureTranslation, bool>>>(), null)).Throws(new Exception("fail"));

        Assert.That(async () => await _service.GetLocalizedMessagesListByKeyAsync(applicationId, key), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "GetLocalizedMessagesListByKeyAsync"), Times.Once);
    }

    [Test]
    public void GetLocalizedMessageByKeyAndParamsSync_ReturnsTranslation_WhenFound()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var key = "key1";
        var value = "Hello";
        var list = new List<CultureTranslationDTO>
        {
            new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = cultureId, TranslationKey = key, TranslationValue = value }
        };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.IsAny<string>())).Returns(list);
        _localizationRepositoryMock.Setup(r => r.AddNewCultureTranslationAsync(It.IsAny<CultureTranslationDTO>(), null)).ReturnsAsync(true);

        var result = _service.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, key);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TranslationValue, Is.EqualTo(value));
    }

    [Test]
    public void GetLocalizedMessageByKeyAndParamsSync_ReturnsMessageNotFound_WhenNotFound()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var key = "notfound";
        var messageNotFound = "Message not found: {0}, {1}";
        var list = new List<CultureTranslationDTO>
        {
            new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = cultureId, TranslationKey = "other", TranslationValue = "Other" },
            new CultureTranslationDTO { ApplicationFk = applicationId, CultureFk = cultureId, TranslationKey = "MessageNotFound", TranslationValue = messageNotFound }
        };
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.IsAny<string>())).Returns(list);
        _localizationRepositoryMock.Setup(r => r.AddNewCultureTranslationAsync(It.IsAny<CultureTranslationDTO>(), null)).ReturnsAsync(true);

        var result = _service.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, key);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TranslationValue, Does.Contain("Message not found"));
    }

    [Test]
    public void GetLocalizedMessageByKeyAndParamsSync_LogsError_OnException()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var key = "key1";
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureTranslationDTO>>(It.IsAny<string>())).Throws(new Exception("fail"));

        Assert.That(() => _service.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, key), Throws.Exception);
        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), "GetLocalizedMessageByKeyAndParamsSync"), Times.Once);
    }
}