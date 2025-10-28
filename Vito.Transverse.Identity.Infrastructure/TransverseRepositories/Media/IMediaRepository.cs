using System.Linq.Expressions;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;

public interface IMediaRepository
{
    Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null);

    Task<bool> AddNewPictureAsync(PictureDTO newRecord, DataBaseServiceContext? context = null);
}
