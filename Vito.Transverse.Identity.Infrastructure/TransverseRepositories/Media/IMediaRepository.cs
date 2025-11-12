using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;

public interface IMediaRepository
{
    Task<List<PictureDTO>> GetPictureList(Expression<Func<Picture, bool>> filters, DataBaseServiceContext? context = null);

    Task<PictureDTO?> GetPictureByIdAsync(long pictureId, DataBaseServiceContext? context = null);

    Task<PictureDTO?> CreateNewPictureAsync(PictureDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<PictureDTO?> UpdatePictureByIdAsync(PictureDTO pictureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeletePictureByIdAsync(long pictureId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);
}
