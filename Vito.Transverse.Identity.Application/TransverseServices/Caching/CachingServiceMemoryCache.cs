using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Entities.Enums;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Caching;

public class CachingServiceMemoryCache(IMemoryCache memoryCache, ICultureRepository cultureRepository, IOptions<MemoryCacheSettingsOptions> memoryCacheSettingsOptions, ILogger<CachingServiceMemoryCache> logger) : ICachingServiceMemoryCache
{
    MemoryCacheSettingsOptions _memoryCacheSettingsOptionsValues = memoryCacheSettingsOptions.Value;

    public bool CacheDataExistsByKey(string itemCacheName)
    {
        bool returValue = false;
        try
        {
            if (_memoryCacheSettingsOptionsValues.IsCacheEnabled)
            {
                var dataExist = memoryCache.TryGetValue(itemCacheName, out object? variable);
                returValue = dataExist;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CacheDataExistsByKey));
        }
        return returValue;
    }

    public bool DeleteCacheDataByKey(string itemCacheKey, bool removeFromSummary = true)
    {
        bool returValue = false;
        try
        {
            if (_memoryCacheSettingsOptionsValues.IsCacheEnabled)
            {
                memoryCache.TryGetValue(itemCacheKey, out object? cacheInfo);
                if (cacheInfo is not null)
                {
                    memoryCache.Remove(itemCacheKey);
                    returValue = true;
                    if (removeFromSummary)
                    {
                        HandleSummaryCache(itemCacheKey, false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCacheDataByKey));
        }
        return returValue;
    }

    public bool ClearCacheData()
    {
        bool returValue = false;
        try
        {
            if (_memoryCacheSettingsOptionsValues.IsCacheEnabled)
            {
                if (memoryCache is MemoryCache concreteMemoryCache)
                {
                    concreteMemoryCache.Clear();
                    returValue = true;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCacheDataByKey));
        }
        return returValue;
    }


    public T? GetCacheDataByKey<T>(string itemCacheName)
    {
        T? returnEntity = (T?)Activator.CreateInstance(typeof(T));
        if (_memoryCacheSettingsOptionsValues.IsCacheEnabled)
        {
            try
            {
                var returnValueByte = memoryCache.Get(itemCacheName);
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
                logger.LogError(ex, message: nameof(GetCacheDataByKey));
                returnEntity = default;
            }
        }
        else
        {
            returnEntity = default;
        }
        return returnEntity!;
    }

    public bool RefreshCacheDataByKey(string itemCacheKey)
    {
        throw new NotImplementedException();
    }

    public bool SetCacheData(string itemCacheName, object itemCacheValue, bool addToSummary = true)
    {
        bool returnValue = false;
        if (_memoryCacheSettingsOptionsValues.IsCacheEnabled)
        {
            try
            {
                var serializedObject = CommonExtensions.Serialize(itemCacheValue);
                var cacheExpiration = cultureRepository.UtcNow().ToLocalTime().AddMinutes(_memoryCacheSettingsOptionsValues.CacheExpirationInMinutes);
                memoryCache.Set(itemCacheName, serializedObject, cacheExpiration);
                returnValue = true;
                if (addToSummary)
                {
                    HandleSummaryCache(itemCacheName, true, itemCacheValue, cacheExpiration);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, message: nameof(SetCacheData));
            }
        }
        return returnValue;
    }

    private void HandleSummaryCache(string itemCacheName, bool isAdd, object? itemCacheValue = null, DateTimeOffset? cacheExpiration = null)
    {
        var summaryCacheList = GetCacheDataByKey<List<CacheSummaryDTO>>(CacheItemKeysEnum.All.ToString());
        if (summaryCacheList is null)
        {
            summaryCacheList = new();
        }
        else
        {
            DeleteCacheDataByKey(CacheItemKeysEnum.All.ToString(), false);
        }

        if (isAdd)
        {
            var numberOfRecords = 0;
            var cacheType = itemCacheValue!.GetType();
            summaryCacheList.Add(
                new()
                {
                    Index = summaryCacheList.Count + 1,
                    Name = itemCacheName,
                    Type = cacheType.FullName!,
                    NumberOfRecords = numberOfRecords,
                    CacheCreation = cacheExpiration!.Value.AddMinutes(-_memoryCacheSettingsOptionsValues.CacheExpirationInMinutes),
                    CacheExpiration = cacheExpiration!.Value,
                    //SerilizedContent = CommonExtensions.Serialize(itemCacheValue)!
                });
        }
        else
        {
            summaryCacheList.RemoveAll(x => x.Name.Equals(itemCacheName));
        }
        SetCacheData(CacheItemKeysEnum.All.ToString(), summaryCacheList, false);
    }





}

public record CacheSummaryDTO
{
    public int Index { get; set; }
    public int NumberOfRecords { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;

    public DateTimeOffset CacheCreation { get; set; }

    public DateTimeOffset CacheExpiration { get; set; }

    public string SerilizedContent { get; set; } = null!;
}
