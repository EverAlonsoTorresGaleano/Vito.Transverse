using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Media;

public interface IMediaService
{

    Task<List<PictureDTO>> GetPictureList(long companyId);

    Task<PictureDTO?> GetPictureByName(long companyId, string name);

    Task<List<PictureDTO>> GetPictureByNameWildCard(long companyId, string wildCard);

    Task<PictureDTO?> GetPictureByIdAsync(long pictureId);

    Task<PictureDTO?> CreateNewPictureAsync(PictureDTO pictureDTO, DeviceInformationDTO deviceInformation);

    Task<PictureDTO?> UpdatePictureByIdAsync(long pictureId, PictureDTO pictureDTO, DeviceInformationDTO deviceInformation);

    Task<bool> DeletePictureByIdAsync(long pictureId, DeviceInformationDTO deviceInformation);

}
