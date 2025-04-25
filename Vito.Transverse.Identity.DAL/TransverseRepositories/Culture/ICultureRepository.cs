using System.Globalization;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;

public interface ICultureRepository
{
    Task<List<CultureDTO>> GetActiveCultureList();

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

    CultureInfo GetCurrentCulture();
    string GetCurrentCultureId();
    string GetCurrentCultureName();
    string SetCurrentCulture(string cultureId);
    //DateTimeOffset NextTokenExpirationTimeFromNow();

    //TimeSpan CacheExpirationTimeFromNow();
    bool DateTimeMomentIsGreaterThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsGreaterOrEqualThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsLowerThanNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentIsLowerOrEqualThanNow(DateTimeOffset dateToCheck);
    TimeSpan DateTimeMomentDiferenceWithNow(DateTimeOffset dateToCheck);
    bool DateTimeMomentNowIsInRange(DateTimeOffset initialMoment, DateTimeOffset endMoment);
}
