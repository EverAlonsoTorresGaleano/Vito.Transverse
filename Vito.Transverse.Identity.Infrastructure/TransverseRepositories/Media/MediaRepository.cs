using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;

public class MediaRepository(IDataBaseContextFactory dataBaseContextFactory, ILogger<MediaRepository> logger) : IMediaRepository
{

    public async Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<PictureDTO>? returnList = null!;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var databaseList = await context.Pictures
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

    public async Task<bool> AddNewPictureAsync(PictureDTO newRecord, DataBaseServiceContext? context = null)
    {
        bool recordSaved = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var dbRecord = newRecord.ToPicture();
            context.Pictures.Add(dbRecord);
            var recordAffected = await context.SaveChangesAsync();
            recordSaved = recordAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return recordSaved;
    }
}
