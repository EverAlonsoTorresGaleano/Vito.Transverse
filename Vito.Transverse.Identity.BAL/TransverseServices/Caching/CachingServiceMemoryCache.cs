using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Caching;

public class CachingServiceMemoryCache(IMemoryCache _memoryCache, ICultureRepository _cultureRepository, IOptions<MemoryCacheSettingsOptions> _memoryCacheSettingsOptions, ILogger<CachingServiceMemoryCache> _logger) : ICachingServiceMemoryCache
{
    MemoryCacheSettingsOptions _memoryCacheSettingsOptionsValues = _memoryCacheSettingsOptions.Value;

    public bool CacheDataExistsByKey(string itemCacheName)
    {
        try
        {
            var dataExist = _memoryCache.TryGetValue(itemCacheName, out object? variable);
            return dataExist;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CacheDataExistsByKey));
            return false;
        }
    }

    public bool DeleteCacheDataByKey(string itemCacheKey)
    {
        try
        {
            _memoryCache.Remove(itemCacheKey);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(DeleteCacheDataByKey));
            return false;
        }
    }



    public T? GetCacheDataByKey<T>(string itemCacheName)
    {
        T? returnEntity = (T?)Activator.CreateInstance(typeof(T));
        try
        {
            var returnValueByte = _memoryCache.Get(itemCacheName);
            var returnValue = returnValueByte?.ToString();
            if (returnValue != null)
            {
                returnEntity = CommonExtensions.Deserialize<T>(returnValue)!;
            }
            else
            {
                returnEntity = default;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetCacheDataByKey));
            returnEntity = default;
        }
        return returnEntity;
    }

    public bool RefreshCacheDataByKey(string itemCacheKey)
    {
        throw new NotImplementedException();
    }

    public bool SetCacheData(string itemCacheName, object itemCacheValue)
    {
        try
        {
            var serializedObject = CommonExtensions.Serialize(itemCacheValue);
            var cacheExpiration = _cultureRepository.UtcNow().AddMinutes(_memoryCacheSettingsOptionsValues.CacheExpirationInMinutes);
            _memoryCache.Set(itemCacheName, serializedObject, cacheExpiration);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(SetCacheData));
            return false;
        }
    }
}
