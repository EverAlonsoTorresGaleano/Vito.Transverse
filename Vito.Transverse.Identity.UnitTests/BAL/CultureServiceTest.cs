using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.UnitTests.BAL;

[TestFixture]
public class CultureServiceTest
{
    private readonly Mock<ICultureRepository> _cultureRepositoryMock;
    private readonly Mock<ILocalizationService> _localizationServiceMock;
    private readonly Mock<ICachingServiceMemoryCache> _cachingServiceMock;
    private readonly Mock<IOptions<CultureSettingsOptions>> _optionsMock;
    private readonly CultureSettingsOptions _settings;
    private readonly CultureService _service;

    public CultureServiceTest()
    {
        _cultureRepositoryMock = new Mock<ICultureRepository>();
        _localizationServiceMock = new Mock<ILocalizationService>();
        _cachingServiceMock = new Mock<ICachingServiceMemoryCache>();
        _settings = new CultureSettingsOptions();
        _optionsMock = new Mock<IOptions<CultureSettingsOptions>>();
        _optionsMock.Setup(x => x.Value).Returns(_settings);

        _service = new CultureService(
            _cultureRepositoryMock.Object,
            _localizationServiceMock.Object,
            _cachingServiceMock.Object,
            _optionsMock.Object
        );
    }

    [Test]
    public void GetCurrectCulture_ReturnsCultureInfo()
    {
        var culture = new CultureInfo("en-US");
        _cultureRepositoryMock.Setup(r => r.GetCurrentCulture()).Returns(culture);

        var result = _service.GetCurrectCulture();

        Assert.That(result, Is.EqualTo(culture));
    }

    [Test]
    public void SetCurrectCulture_ReturnsString()
    {
        _cultureRepositoryMock.Setup(r => r.SetCurrentCulture("en-US")).Returns("en-US");

        var result = _service.SetCurrectCulture("en-US");

        Assert.That(result, Is.EqualTo("en-US"));
    }

    [Test]
    public void UtcNow_ReturnsDateTimeOffset()
    {
        var now = DateTimeOffset.UtcNow;
        _cultureRepositoryMock.Setup(r => r.UtcNow()).Returns(now);

        var result = _service.UtcNow();

        Assert.That(result, Is.EqualTo(now));
    }

    [Test]
    public async Task GetActiveCultureListItemDTOListAsync_ReturnsLocalizedList()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var cultureList = new List<CultureDTO>
        {
            new CultureDTO { Id = "en-US", NameTranslationKey = "key1", Name = "English" }
        };
        var listItemList = new List<ListItemDTO>
        {
            new ListItemDTO { Id = "en-US", NameTranslationKey = "key1", IsEnabled = true }
        };
        var localized = new CultureTranslationDTO { TranslationValue = "English" };

        _cultureRepositoryMock.Setup(r => r.GetCurrentCultureId()).Returns(cultureId);
        _service.GetType().GetMethod("GetActiveCultureListAsync")!
            .Invoke(_service, new object[] { applicationId }); // To ensure method exists
        _cultureRepositoryMock.Setup(r => r.GetActiveCultureListAsync()).ReturnsAsync(cultureList);
        _localizationServiceMock.Setup(l => l.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, "key1"))
            .Returns(localized);

        // Patch ToListItemDTOList extension
        // We'll just simulate the result of ToListItemDTOList
        // So we mock GetActiveCultureListAsync to return a list, and then we patch the localization call

        // Act
        var result = await _service.GetActiveCultureListItemDTOListAsync(applicationId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].NameTranslationKey, Is.EqualTo("English"));
    }

    [Test]
    public async Task GetActiveCultureListAsync_ReturnsFromCache_WhenCacheExists()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var cacheKey = CacheItemKeysEnum.CultureList.ToString() + applicationId;
        var cachedList = new List<CultureDTO>
        {
            new CultureDTO { Id = "en-US", NameTranslationKey = "key1", Name = "English" }
        };

        _cultureRepositoryMock.Setup(r => r.GetCurrentCultureId()).Returns(cultureId);
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureDTO>>(cacheKey)).Returns(cachedList);

        var result = await _service.GetActiveCultureListAsync(applicationId);

        Assert.That(result, Is.EqualTo(cachedList));
        _cachingServiceMock.Verify(c => c.GetCacheDataByKey<List<CultureDTO>>(cacheKey), Times.Once);
        _cultureRepositoryMock.Verify(r => r.GetActiveCultureListAsync(), Times.Never);
    }

    [Test]
    public async Task GetActiveCultureListAsync_FetchesAndCaches_WhenCacheIsNull()
    {
        var applicationId = 1L;
        var cultureId = "en-US";
        var cacheKey = CacheItemKeysEnum.CultureList.ToString() + applicationId;
        var repoList = new List<CultureDTO>
        {
            new CultureDTO { Id = "en-US", NameTranslationKey = "key1", Name = "English" }
        };
        var localized = new CultureTranslationDTO { TranslationValue = "English" };

        _cultureRepositoryMock.Setup(r => r.GetCurrentCultureId()).Returns(cultureId);
        _cachingServiceMock.Setup(c => c.GetCacheDataByKey<List<CultureDTO>>(cacheKey)).Returns((List<CultureDTO>)null);
        _cultureRepositoryMock.Setup(r => r.GetActiveCultureListAsync()).ReturnsAsync(repoList);
        _localizationServiceMock.Setup(l => l.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, "key1"))
            .Returns(localized);
        _cachingServiceMock.Setup(c => c.SetCacheData(cacheKey, repoList, true)).Returns(true);

        var result = await _service.GetActiveCultureListAsync(applicationId);

        Assert.That(result, Is.EqualTo(repoList));
        _cachingServiceMock.Verify(c => c.SetCacheData(cacheKey, repoList, true), Times.Once);
    }
}