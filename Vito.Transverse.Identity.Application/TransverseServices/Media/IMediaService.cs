using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Media;

public interface IMediaService
{

    Task<List<PictureDTO>> GetPictureList(long companyId);

    Task<PictureDTO?> GetPictureByName(long companyId, string name);

    Task<List<PictureDTO>> GetPictureByNameWildCard(long companyId, string wildCard);

}
