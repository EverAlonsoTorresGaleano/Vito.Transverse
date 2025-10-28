using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.UnitTests.Helpers;

namespace Vito.Transverse.Identity.UnitTests.DAL;

[TestFixture]
public class CultureRepositoryTest
{
    private IDataBaseContextFactory _dbContextFactoryMock;
    private Mock<ILogger< Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization.LocalizationRepository>> _loggerMock = new();
    private CultureRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _repository = new CultureRepository(_dbContextFactoryMock, _loggerMock.Object);
    }

    [Test]
    public async Task GetActiveCultureListAsync_ReturnsActiveCultures()
    {
        var result = await _repository.GetActiveCultureListAsync();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<CultureDTO>>());
    }

    [Test]
    public void UtcNow_ReturnsCurrentUtcTime()
    {
        var now = _repository.UtcNow();
        Assert.That(now, Is.TypeOf<DateTimeOffset>());
        Assert.That(now, Is.EqualTo(DateTimeOffset.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void MomentToString_ReturnsString()
    {
        var now = DateTimeOffset.UtcNow;
        var result = _repository.MomentToString(now);
        Assert.That(result, Is.EqualTo(now.ToString()));
    }

    [Test]
    public void MomentToLongDateTimeString_ReturnsFormattedString()
    {
        var now = DateTimeOffset.UtcNow;
        var expected = now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern);
        var result = _repository.MomentToLongDateTimeString(now);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MomentToLongDateString_ReturnsFormattedString()
    {
        var now = DateTimeOffset.UtcNow;
        var expected = now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
        var result = _repository.MomentToLongDateString(now);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MomentToLongTimeString_ReturnsFormattedString()
    {
        var now = DateTimeOffset.UtcNow;
        var expected = now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);
        var result = _repository.MomentToLongTimeString(now);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MomentToShortDateString_ReturnsFormattedString()
    {
        var now = DateTimeOffset.UtcNow;
        var expected = now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        var result = _repository.MomentToShortDateString(now);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MomentToShortTimeString_ReturnsFormattedString()
    {
        var now = DateTimeOffset.UtcNow;
        var expected = now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
        var result = _repository.MomentToShortTimeString(now);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void NowToString_ReturnsNowAsString()
    {
        var result = _repository.NowToString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void NowToLongDateTimeString_ReturnsNowAsLongDateTimeString()
    {
        var result = _repository.NowToLongDateTimeString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void NowToLongDateString_ReturnsNowAsLongDateString()
    {
        var result = _repository.NowToLongDateString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void NowToLongTimeString_ReturnsNowAsLongTimeString()
    {
        var result = _repository.NowToLongTimeString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void NowToShortDateString_ReturnsNowAsShortDateString()
    {
        var result = _repository.NowToShortDateString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void NowToShortTimeString_ReturnsNowAsShortTimeString()
    {
        var result = _repository.NowToShortTimeString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetCurrentCulture_ReturnsCurrentCulture()
    {
        var result = _repository.GetCurrentCulture();
        Assert.That(result, Is.EqualTo(Thread.CurrentThread.CurrentCulture));
    }

    [Test]
    public void GetCurrentCultureId_ReturnsCurrentCultureId()
    {
        var result = _repository.GetCurrentCultureId();
        Assert.That(result, Is.EqualTo(Thread.CurrentThread.CurrentCulture.Name));
    }

    [Test]
    public void GetCurrentCultureName_ReturnsCurrentCultureName()
    {
        var result = _repository.GetCurrentCultureName();
        Assert.That(result, Is.EqualTo(Thread.CurrentThread.CurrentCulture.DisplayName));
    }

    [Test]
    public void SetCurrentCulture_ChangesCulture()
    {
        var original = Thread.CurrentThread.CurrentCulture;
        var newCulture = "fr-FR";
        var result = _repository.SetCurrentCulture(newCulture);
        Assert.That(result, Is.EqualTo(newCulture));
        Assert.That(Thread.CurrentThread.CurrentCulture.Name, Is.EqualTo(newCulture));
        Thread.CurrentThread.CurrentCulture = original;
        Thread.CurrentThread.CurrentUICulture = original;
    }

    [Test]
    public void DateTimeMomentIsGreaterThanNow_ReturnsExpected()
    {
        var future = DateTimeOffset.UtcNow.AddMinutes(1);
        var past = DateTimeOffset.UtcNow.AddMinutes(-1);
        Assert.That(_repository.DateTimeMomentIsGreaterThanNow(future), Is.True);
        Assert.That(_repository.DateTimeMomentIsGreaterThanNow(past), Is.False);
    }

    [Test]
    public void DateTimeMomentIsGreaterOrEqualThanNow_ReturnsExpected()
    {
        var future = DateTimeOffset.UtcNow.AddMinutes(1);
        var now = DateTimeOffset.UtcNow;
        Assert.That(_repository.DateTimeMomentIsGreaterOrEqualThanNow(future), Is.True);
        Assert.That(_repository.DateTimeMomentIsGreaterOrEqualThanNow(now), Is.True);
    }

    [Test]
    public void DateTimeMomentIsLowerThanNow_ReturnsExpected()
    {
        var past = DateTimeOffset.UtcNow.AddMinutes(-1);
        var future = DateTimeOffset.UtcNow.AddMinutes(1);
        Assert.That(_repository.DateTimeMomentIsLowerThanNow(past), Is.True);
        Assert.That(_repository.DateTimeMomentIsLowerThanNow(future), Is.False);
    }

    [Test]
    public void DateTimeMomentIsLowerOrEqualThanNow_ReturnsExpected()
    {
        var past = DateTimeOffset.UtcNow.AddMinutes(-1);
        var now = DateTimeOffset.UtcNow;
        Assert.That(_repository.DateTimeMomentIsLowerOrEqualThanNow(past), Is.True);
        Assert.That(_repository.DateTimeMomentIsLowerOrEqualThanNow(now), Is.True);
    }

    [Test]
    public void DateTimeMomentDiferenceWithNow_ReturnsTimeSpan()
    {
        var past = DateTimeOffset.UtcNow.AddMinutes(-5);
        var result = _repository.DateTimeMomentDiferenceWithNow(past);
        Assert.That(result, Is.TypeOf<TimeSpan>());
        Assert.That(result.TotalMinutes, Is.GreaterThan(4));
    }

    [Test]
    public void DateTimeMomentNowIsInRange_ReturnsExpected()
    {
        var now = DateTimeOffset.UtcNow;
        var before = now.AddMinutes(-1);
        var after = now.AddMinutes(1);
        Assert.That(_repository.DateTimeMomentNowIsInRange(before, after), Is.True);
        Assert.That(_repository.DateTimeMomentNowIsInRange(after, before), Is.False);
    }
}