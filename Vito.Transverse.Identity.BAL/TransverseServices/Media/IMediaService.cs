using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Media;

public interface IMediaService
{

    Task<List<PictureDTO>> GetPictureList(long companyId);

    Task<PictureDTO> GetPictureByName(long companyId, string name);

    Task<List<PictureDTO>> GetPictureByNameWildCard(long companyId, string wildCard);

}
