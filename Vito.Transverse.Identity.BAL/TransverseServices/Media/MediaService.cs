using Microsoft.Extensions.Logging;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Media;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Media;

public class MediaService(IMediaRepository mediaRepository, ICachingServiceMemoryCache cachingServiceMemoryCache, ILogger<MediaService> logger) : IMediaService
{
    public async Task<PictureDTO> GetPictureByName(long companyId, string name)
    {
        try
        {
            var companyList = await GetPictureList(companyId);
            var returnItem = companyList.FirstOrDefault(x => x.Name.Equals(name));
            return returnItem;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureByName));
            throw;
        }
    }

    public async Task<List<PictureDTO>> GetPictureByNameWildCard(long companyId, string wildCard)
    {
        try
        {
            var companyList = await GetPictureList(companyId);
            var returnList = companyList.Where(x => x.Name.Contains(wildCard, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureByName));
            throw;
        }
    }

    public async Task<List<PictureDTO>> GetPictureList(long companyId)
    {
        try
        {
            var returnList = cachingServiceMemoryCache.GetCacheDataByKey<List<PictureDTO>>(CacheItemKeysEnum.PictureListByCompanyId + companyId.ToString());
            if (returnList is null)
            {
                returnList = await mediaRepository.GetPictureList(companyId);
                cachingServiceMemoryCache.SetCacheData(CacheItemKeysEnum.PictureListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureByName));
            throw;
        }
    }
}
