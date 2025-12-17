using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Index.Quadtree;
using System;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;

namespace Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;

public class MediaRepository(IDataBaseContextFactory dataBaseContextFactory, ICultureRepository cultureRepository, ILogger<MediaRepository> logger) : IMediaRepository
{

    public async Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<PictureDTO>? returnList = null!;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            //DwnloadCountriesPictureFileFromCloud();
            //PopulatePictureTableWithCounrtriesPictures();




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


    private async void DwnloadCountriesPictureFileFromCloud()
    {
        var context = dataBaseContextFactory.GetDbContext();
        var cuntriesList = await context.Countries.ToListAsync();


        cuntriesList.ForEach(async itemCountry =>
        {
            var name = $"{itemCountry.Id}.png";
            await DownloadPictureFileFromCloud(name);
        });
    }

    private async void PopulatePictureTableWithCounrtriesPictures()
    {
        var context = dataBaseContextFactory.GetDbContext();
        var cuntriesList = await context.Countries.ToListAsync();

        cuntriesList.ForEach(async itemCountry =>
        {
            var name = $"{itemCountry.Id}.png";
            var fileName = "C:\\Repos\\Temp\\" + name;

            if (File.Exists(fileName))
            {
                var fileContent = File.ReadAllBytes(fileName);
                await CreateNewPictureAsync(new PictureDTO
                {
                    Name = name,
                    CompanyFk = 1,
                    EntityFk = null,
                    FileTypeFk = 801,
                    PictureCategoryFk = 909,
                    PictureSize = fileContent.Length,
                    BinaryPicture = fileContent,
                    IsActive = true

                }, new DeviceInformationDTO { UserId = 1 });

            }
        });
    }

    private async Task<bool> DownloadPictureFileFromCloud(string name)
    {
        try
        {
            var url = "https://img.mobiscroll.com/demos/flags/";
            using (HttpClient client = new HttpClient())
            {
                // Send a GET request and get the response as a stream
                using (Stream contentStream = await client.GetStreamAsync(url + name))
                {
                    // Create a local file stream to write the downloaded content
                    using (FileStream fileStream = new FileStream("C:\\Repos\\Temp\\" + name, FileMode.Create, FileAccess.Write))
                    {
                        // Copy the content stream to the file stream
                        await contentStream.CopyToAsync(fileStream);
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;

        }
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
