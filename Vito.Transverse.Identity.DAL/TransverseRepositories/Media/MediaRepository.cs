using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Media;

public class MediaRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository cultureRepository, ILogger<MediaRepository> Logger) : IMediaRepository
{
    public async Task<List<PictureDTO>> GetPictureList(long companyId, DataBaseServiceContext? context = null)
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
                .Where(x => x.CompanyFk == companyId).ToListAsync();

            returnList = databaseList.ToPictureDTOList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return returnList!;
    }


    public async Task<bool?> SavePictureAsync(PictureDTO picture, DataBaseServiceContext? context = null)
    {
        bool? pictureSaved = null!;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);

            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\ever.torresg\EATGSoft\Projects\Gestion Inmobiliaria Web\Codigo\EATG.GI.Web\Imagenes\Sistema\LogoFacebook.png");
            string bytesStr = string.Join(",", bytes);

            var modelDb = picture.ToPicture();
            modelDb = new Picture()
            {
                EntityFk = 1,
                FileTypeFk = (long)FileTypeEnum.FileType_Jpg,
                PictureCategoryFk = (long)PictureCategoryTypeEnum.PictureCategoryType_System,
                Id = 2,
                CreatedByUserFk = 1,
                CreationDate = cultureRepository.UtcNow().DateTime,
                IsActive = true,
                Name = "LaPic",
                PictureSize = 1026,
                BinaryPicture = bytes
            };

            context.Pictures.Add(modelDb);
            var recordAffected = await context.SaveChangesAsync();
            pictureSaved = recordAffected > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, nameof(GetPictureList));
            throw;
        }
        return pictureSaved!;
    }



}
