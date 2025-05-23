using System.Linq.Expressions;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Media;

public interface IMediaRepository
{
    Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null);

    Task<bool> AddNewPictureAsync(PictureDTO newRecord, DataBaseServiceContext? context = null);
}
