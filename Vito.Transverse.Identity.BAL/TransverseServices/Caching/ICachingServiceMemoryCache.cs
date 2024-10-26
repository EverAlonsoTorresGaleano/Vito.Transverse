using Vito.Transverse.Identity.Domain.Enums;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Caching;

public interface ICachingServiceMemoryCache
{
    /// <summary>
    /// Verify If here Data on cache for specific Key.
    /// </summary>
    /// <param name="itemCacheKey"></param>
    /// <returns></returns>
    bool CacheDataExistsByKey(string itemCacheKey);

    /// <summary>
    /// Get Data Stored on Cache by ItemCache Key IS required serialize response string
    /// </summary>
    /// <param name="itemCacheName"></param>
    /// <returns>Serialized Cahe Data</returns>
    T GetCacheDataByKey<T>(string itemCacheName);


    /// <summary>
    /// Save Data on Cache by Key
    /// </summary>
    /// <param name="itemCacheName"></param>
    /// <param name="itemCache"></param>
    /// <returns>Tru iff sucees otherwize false.</returns>
    bool SetCacheData(string itemCacheName, object itemCache);


    /// <summary>
    /// Delete data on cache by Key
    /// </summary>
    /// <param name="itemCacheKey"></param>
    /// <returns></returns>
    bool DeleteCacheDataByKey(string itemCacheKey);

    /// <summary>
    /// Refersh Cache
    /// </summary>
    /// <param name="itemCacheKey"></param>
    /// <returns></returns>
    bool RefreshCacheDataByKey(string itemCacheKey);
}
