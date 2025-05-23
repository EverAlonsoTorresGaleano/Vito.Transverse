using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Media;

public class MediaRepository(IDataBaseContextFactory _dataBaseContextFactory, ILogger<MediaRepository> Logger) : IMediaRepository
{

    public async Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<PictureDTO>? returnList = null!;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var databaseList = await context.Pictures
                    .Include(x => x.CompanyFkNavigation)
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.FileTypeFkNavigation)
                .Include(x => x.PictureCategoryFkNavigation)
                .Where(filters).ToListAsync();

            returnList = databaseList.ToPictureDTOList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return returnList!;
    }

    public async Task<bool> AddNewPictureAsync(PictureDTO newRecord, DataBaseServiceContext? context = null)
    {
        bool recordSaved = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var dbRecord = newRecord.ToPicture();
            context.Pictures.Add(dbRecord);
            var recordAffected = await context.SaveChangesAsync();
            recordSaved = recordAffected > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return recordSaved;
    }
}
