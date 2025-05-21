using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Media;

public interface IMediaRepository
{
    Task<List<PictureDTO>> GetPictureList(long companyId, DataBaseServiceContext? context = null);

    Task<bool?> SavePictureAsync(PictureDTO picture, DataBaseServiceContext? context = null);
}
