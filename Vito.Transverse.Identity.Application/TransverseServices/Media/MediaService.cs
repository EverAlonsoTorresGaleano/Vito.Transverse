using Microsoft.Extensions.Logging;
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
            logger.LogError(ex, nameof(GetPictureByName));
            throw;
        }
    }

    public async Task<bool?> AddNewPictureAsync(PictureDTO picture)
    {
        bool? pictureSaved = null!;
        try
        {
//#if DEBUG
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\ever.torresg\EATGSoft\Projects\Gestion Inmobiliaria Web\Codigo\EATG.GI.Web\Imagenes\Sistema\LogoFacebook.png");
            string bytesStr = string.Join(",", bytes);

            var newPicture = new PictureDTO()
            {
                EntityFk = 1,
                FileTypeFk = (long)FileTypeEnum.FileType_Jpg,
                PictureCategoryFk = (long)PictureCategoryTypeEnum.PictureCategoryType_System,
                Id = 2,
                CreatedByUserFk = 1,
                IsActive = true,
                Name = "LaPic",
                PictureSize = 1026,
                BinaryPicture = bytes
            };
//#endif
            newPicture.CreationDate = cultureRepository.UtcNow().DateTime;
            pictureSaved = await mediaRepository.AddNewPictureAsync(newPicture);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return pictureSaved!;
    }


}
