using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Media;

public class MediaService(IMediaRepository mediaRepository, ICachingServiceMemoryCache cachingServiceMemoryCache, ICultureRepository cultureRepository, ILogger<MediaService> logger) : IMediaService
{
    public async Task<PictureDTO?> GetPictureByName(long companyId, string name)
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
                returnList = await mediaRepository.GetPictureList(x => x.CompanyFk == companyId);
                cachingServiceMemoryCache.SetCacheData(CacheItemKeysEnum.PictureListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
    }

    public async Task<PictureDTO?> GetPictureByIdAsync(long pictureId)
    {
        try
        {
            return await mediaRepository.GetPictureByIdAsync(pictureId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureByIdAsync));
            throw;
        }
    }

    public async Task<PictureDTO?> CreateNewPictureAsync(PictureDTO pictureDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await mediaRepository.CreateNewPictureAsync(pictureDTO, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(CreateNewPictureAsync));
            throw;
        }
    }

    public async Task<PictureDTO?> UpdatePictureByIdAsync(long pictureId, PictureDTO pictureDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            pictureDTO.Id = pictureId;
            return await mediaRepository.UpdatePictureByIdAsync(pictureDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(UpdatePictureByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeletePictureByIdAsync(long pictureId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await mediaRepository.DeletePictureByIdAsync(pictureId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(DeletePictureByIdAsync));
            throw;
        }
    }

}
