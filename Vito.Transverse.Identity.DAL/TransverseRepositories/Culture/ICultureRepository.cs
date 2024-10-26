using System.Globalization;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;

public interface ICultureRepository
{
    DateTimeOffset UtcNow();
    string MomentToString(DateTimeOffset currentDateTime);
    string MomentToLongDateTimeString(DateTimeOffset currentDateTime);
    string MomentToLongDateString(DateTimeOffset currentDateTime);
    string MomentToLongTimeString(DateTimeOffset currentDateTime);
    string MomentToShortDateString(DateTimeOffset currentDateTime);
    string MomentToShortTimeString(DateTimeOffset currentDateTime);
    string NowToString();
    string NowToLongDateTimeString();
    string NowToLongDateString();
    string NowToLongTimeString();
    string NowToShortDateString();
    string NowToShortTimeString();

    CultureInfo GetCurrectCulture();
    string GetCurrectCultureId();
    string GetCurrectCultureName();
    string SetCurrectCulture(string cultureId);
    //DateTimeOffset NextTokenExpirationTimeFromNow();

    //TimeSpan CacheExpirationTimeFromNow();
    bool DateTimeMomentIsGreaterThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsGreaterOrEqualThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsLowerThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsLowerOrEqualThanNow(DateTimeOffset dateToCheck);
    TimeSpan DateTimeMomentDiferenceWithNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentNowIsInRange(DateTimeOffset initialMoment, DateTimeOffset endMoment);
}
