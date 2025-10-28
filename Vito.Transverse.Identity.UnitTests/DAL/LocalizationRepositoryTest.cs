using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.UnitTests.Helpers;

namespace Vito.Transverse.Identity.UnitTests.DAL;

[TestFixture]
public class LocalizationRepositoryTest
{
    private IDataBaseContextFactory _dbContextFactoryMock;
    private Mock<IOptions<CultureSettingsOptions>> _optionsMock = new();
    private Mock<ILogger<LocalizationRepository>> _loggerMock = new();
    private LocalizationRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _optionsMock.Setup(x => x.Value).Returns(new CultureSettingsOptions());
        _repository = new LocalizationRepository(_dbContextFactoryMock, _optionsMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetLocalizedMessagesListAsync_ReturnsList()
    {
        Expression<Func<CultureTranslation, bool>> filter = x => true;
        var result = await _repository.GetLocalizedMessagesListAsync(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<CultureTranslationDTO>>());
    }

    [Test]
    public async Task AddNewCultureTranslationAsync_ReturnsTrue()
    {
        var dto = new CultureTranslationDTO
        {
            CultureFk = "en-US",
            TranslationKey = "TestKey",
            TranslationValue = "TestValue"
        };
        var result = await _repository.AddNewCultureTranslationAsync(dto);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UpdateCultureTranslationAsync_ReturnsTrue()
    {
        var dto = new CultureTranslationDTO
        {
            CultureFk = CultureInfo.CurrentCulture.Name,
            TranslationKey = "TestKey",
            TranslationValue = "UpdatedValue"
        };
        var result = await _repository.UpdateCultureTranslationAsync(dto);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteCultureTranslationAsync_ReturnsTrue()
    {
        var key = "TestKey";
        var result = await _repository.DeleteCultureTranslationAsync(key);
        Assert.That(result, Is.True);
    }
}