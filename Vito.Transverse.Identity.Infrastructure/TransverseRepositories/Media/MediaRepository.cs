using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;

public class MediaRepository(IDataBaseContextFactory dataBaseContextFactory,ICultureRepository cultureRepository, ILogger<MediaRepository> logger) : IMediaRepository
{

    public async Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<PictureDTO>? returnList = null!;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var databaseList = await context.Pictures.AsNoTracking()
                    .Include(x => x.CompanyFkNavigation)
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.FileTypeFkNavigation)
                .Include(x => x.PictureCategoryFkNavigation)
                .Where(filters).ToListAsync();

            returnList = databaseList.Select(x => x.ToPictureDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return returnList!;
    }

    public async Task<PictureDTO?> GetPictureByIdAsync(long pictureId, DataBaseServiceContext? context = null)
    {
        PictureDTO? pictureDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var picture = await context.Pictures.AsNoTracking()
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.FileTypeFkNavigation)
                .Include(x => x.PictureCategoryFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == pictureId);
            pictureDTO = picture?.ToPictureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureByIdAsync));
            throw;
        }
        return pictureDTO;
    }

    public async Task<PictureDTO?> CreateNewPictureAsync(PictureDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        PictureDTO? savedRecord = null;
        var newRecordDb = newRecord.ToPicture();
        newRecordDb.CreationDate = cultureRepository.UtcNow().DateTime;
        newRecordDb.CreatedByUserFk = deviceInformation.UserId;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Pictures.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToPictureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(CreateNewPictureAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<PictureDTO?> UpdatePictureByIdAsync(PictureDTO pictureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        PictureDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var pictureToUpdate = await context.Pictures.FirstOrDefaultAsync(x => x.Id == pictureInfo.Id);
            if (pictureToUpdate is null)
            {
                return null;
            }

            var updatedPicture = pictureInfo.ToPicture();
            updatedPicture.LastUpdateDate = cultureRepository.UtcNow().DateTime;
            updatedPicture.LastUpdateByUserFk = deviceInformation.UserId;
            context.Entry(pictureToUpdate).CurrentValues.SetValues(updatedPicture);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            pictureToUpdate = await context.Pictures
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.FileTypeFkNavigation)
                .Include(x => x.PictureCategoryFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == pictureInfo.Id);
            
            savedRecord = pictureToUpdate?.ToPictureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(UpdatePictureByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeletePictureByIdAsync(long pictureId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var picture = await context.Pictures.FirstOrDefaultAsync(x => x.Id == pictureId);
            if (picture is null)
            {
                return false;
            }

            context.Pictures.Remove(picture);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(DeletePictureByIdAsync));
            throw;
        }

        return deleted;
    }
}
