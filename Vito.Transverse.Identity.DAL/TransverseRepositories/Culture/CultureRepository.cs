using System.Globalization;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;

public class CultureRepository : ICultureRepository
{
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
        var cultureInfo = GetCurrectCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.FullDateTimePattern);
        return returnValue;
    }

    public string MomentToLongDateString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrectCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.LongDatePattern);
        return returnValue;
    }

    public string MomentToLongTimeString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrectCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.LongTimePattern);
        return returnValue;
    }

    public string MomentToShortDateString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrectCulture();
        var returnValue = currentDateTime.ToString(cultureInfo.DateTimeFormat.ShortDatePattern);
        return returnValue;
    }

    public string MomentToShortTimeString(DateTimeOffset currentDateTime)
    {
        var cultureInfo = GetCurrectCulture();
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

    public CultureInfo GetCurrectCulture()
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        return currentCulture;
    }

    public string GetCurrectCultureId()
    {
        var currentCulture = GetCurrectCulture().Name;
        return currentCulture;
    }

    public string GetCurrectCultureName()
    {
        var currentCulture = GetCurrectCulture().DisplayName;
        return currentCulture;
    }

    public string SetCurrectCulture(string cultureId)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureId);
        var returnValue = GetCurrectCultureId();
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
