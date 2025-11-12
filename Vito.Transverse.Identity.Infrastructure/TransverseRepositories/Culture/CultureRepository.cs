using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;

public class CultureRepository(IDataBaseContextFactory dataBaseContextFactory, ILogger<LocalizationRepository> logger) : ICultureRepository
{

    public async Task<List<CultureDTO>> GetActiveCultureListAsync()
    {

        List<CultureDTO> returnListDTO = default!;
        DataBaseServiceContext context = default!;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var returnList = await context.Cultures.Where(x => x.IsEnabled).ToListAsync();
            returnListDTO = returnList.Select(x => x.ToCultureDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetActiveCultureListAsync));
        }
        finally
        {
            context.Dispose();
        }
        return returnListDTO;
    }

    public DateTimeOffset UtcNow()
    {
        var returnValue = DateTimeOffset.UtcNow;
        return returnValue;
    }

    public string MomentToString(DateTimeOffset currentDateTime)
    {
        var returnValue = currentDateTime.ToString();
        return returnValue;
    }

    public string MomentToLongDateTimeString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrentCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.FullDateTimePattern);
        return returnValue;
    }

    public string MomentToLongDateString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrentCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.LongDatePattern);
        return returnValue;
    }

    public string MomentToLongTimeString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrentCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.LongTimePattern);
        return returnValue;
    }

    public string MomentToShortDateString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrentCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.ShortDatePattern);
        return returnValue;
    }

    public string MomentToShortTimeString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrentCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.ShortTimePattern);
        return returnValue;
    }

    public string NowToString()
    {
        var returnValue = MomentToString(UtcNow());
        return returnValue;
    }

    public string NowToLongDateTimeString()
    {
        var returnValue = MomentToLongDateTimeString(UtcNow());
        return returnValue;
    }

    public string NowToLongDateString()
    {
        var returnValue = MomentToLongDateString(UtcNow());
        return returnValue;
    }

    public string NowToLongTimeString()
    {
        var returnValue = MomentToLongTimeString(UtcNow());
        return returnValue;
    }

    public string NowToShortDateString()
    {
        var returnValue = MomentToShortDateString(UtcNow());
        return returnValue;
    }

    public string NowToShortTimeString()
    {
        var returnValue = MomentToShortTimeString(UtcNow());
        return returnValue;
    }

    public CultureInfo GetCurrentCulture()
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        return currentCulture;
    }

    public string GetCurrentCultureId()
    {
        var currentCulture = GetCurrentCulture().Name;
        return currentCulture;
    }

    public string GetCurrentCultureName()
    {
        var currentCulture = GetCurrentCulture().DisplayName;
        return currentCulture;
    }

    public string SetCurrentCulture(string cultureId)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureId);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureId);
        var returnValue = GetCurrentCultureId();
        return returnValue;
    }

    //public DateTimeOffset NextTokenExpirationTimeFromNow()
    //{
    //    var returnValue = Now().AddSeconds(int.Parse(_configuration[ConstantsAppSettings.WEBAPI_TOKEN_EXPIRATION])); ;
    //    return returnValue;
    //}

    //public TimeSpan CacheExpirationTimeFromNow()
    //{
    //    var returnValue = TimeSpan.FromSeconds(int.Parse(_configuration[ConstantsAppSettings.WEBAPI_TOKEN_EXPIRATION])); ;
    //    return returnValue;
    //}


    public bool DateTimeMomentIsGreaterThanNow(DateTimeOffset dateToCheck)
    {
        var returnValue = UtcNow() < dateToCheck;
        return returnValue;
    }

    public bool DateTimeMomentIsGreaterOrEqualThanNow(DateTimeOffset dateToCheck)
    {
        var returnValue = UtcNow() <= dateToCheck;
        return returnValue;
    }

    public bool DateTimeMomentIsLowerThanNow(DateTimeOffset dateToCheck)
    {
        var returnValue = UtcNow() > dateToCheck;
        return returnValue;
    }

    public bool DateTimeMomentIsLowerOrEqualThanNow(DateTimeOffset dateToCheck)
    {
        var returnValue = UtcNow() >= dateToCheck;
        return returnValue;
    }

    public TimeSpan DateTimeMomentDiferenceWithNow(DateTimeOffset dateToCheck)
    {
        var returnValue = UtcNow() - dateToCheck;
        return returnValue;
    }

    public bool DateTimeMomentNowIsInRange(DateTimeOffset initialMoment, DateTimeOffset endMoment)
    {
        var returnValue = DateTimeMomentIsLowerOrEqualThanNow(initialMoment) && DateTimeMomentIsGreaterOrEqualThanNow(endMoment);
        return returnValue;
    }
}
