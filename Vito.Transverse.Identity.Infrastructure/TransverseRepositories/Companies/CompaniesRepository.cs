using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Extensions;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Security;

namespace Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;

public class CompaniesRepository(ILogger<SecurityRepository> logger, ICultureRepository cultureRepository, IDataBaseContextFactory dataBaseContextFactory) : ICompaniesRepository
{


    public async Task<CompanyDTO?> CreateNewCompanyAsync(CompanyDTO newRecord)
    {
        CompanyDTO? savedRecord = null;
        var newRecordDb = newRecord.ToCompany();
        try
        {
            var context = dataBaseContextFactory.GetDbContext();
            context.Companies.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
        }
        return savedRecord;
    }

    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyMembershipsDTO> applicationDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyMembershipsList = await context.CompanyMemberships.AsNoTracking()
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.OwnerFkNavigation)
                .Include(x => x.MembershipTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            applicationDTOList = companyMembershipsList.Select(x => x.ToCompanyMembershipsDTO()).ToList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyMemberhipListAsync));
        }

        return applicationDTOList;
    }

    public async Task<CompanyMembershipsDTO?> GetCompanyMembershipByIdAsync(long membershipId, DataBaseServiceContext? context = null)
    {
        CompanyMembershipsDTO? membershipDTO = null;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var membership = await context.CompanyMemberships.AsNoTracking()
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.OwnerFkNavigation)
                .Include(x => x.MembershipTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == membershipId);
            membershipDTO = membership?.ToCompanyMembershipsDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyMembershipByIdAsync));
        }

        return membershipDTO;
    }

    public async Task<CompanyMembershipsDTO?> UpdateCompanyMembershipByIdAsync(CompanyMembershipsDTO membershipInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyMembershipsDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var membershipToUpdate = await context.CompanyMemberships.FirstOrDefaultAsync(x => x.Id == membershipInfo.Id);
            if (membershipToUpdate is null)
            {
                return null;
            }

            membershipToUpdate.CompanyFk = membershipInfo.CompanyFk;
            membershipToUpdate.ApplicationFk = membershipInfo.ApplicationFk;
            membershipToUpdate.MembershipTypeFk = membershipInfo.MembershipTypeFk;
            membershipToUpdate.StartDate = membershipInfo.StartDate;
            membershipToUpdate.EndDate = membershipInfo.EndDate;
            membershipToUpdate.IsActive = membershipInfo.IsActive;
            membershipToUpdate.LastUpdateDate = cultureRepository.UtcNow().DateTime;
            membershipToUpdate.LastUpdateByUserFk = deviceInformation.UserId;

            await context.SaveChangesAsync();

            // Reload with includes to get navigation properties
            membershipToUpdate = await context.CompanyMemberships
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.OwnerFkNavigation)
                .Include(x => x.MembershipTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == membershipInfo.Id);

            savedRecord = membershipToUpdate?.ToCompanyMembershipsDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyMembershipByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteCompanyMembershipByIdAsync(long membershipId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var membership = await context.CompanyMemberships.FirstOrDefaultAsync(x => x.Id == membershipId);
            if (membership is null)
            {
                return false;
            }

            context.CompanyMemberships.Remove(membership);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCompanyMembershipByIdAsync));
            throw;
        }

        return deleted;
    }

    public async Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyDTO> companyDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyList = await context.Companies.AsNoTracking()

                .Include(x => x.CountryFkNavigation)
                .Include(x => x.DefaultCultureFkNavigation)
                .ThenInclude(x => x.LanguageFkNavigation)
                .Where(filters)
                .ToListAsync();
            companyDTOList = companyList.Select(x => x.ToCompanyDTO()).ToList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
        }

        return companyDTOList;
    }

    public async Task<CompanyDTO?> GetCompanyByIdAsync(long companyId, DataBaseServiceContext? context = null)
    {
        CompanyDTO? companyDTO = null;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var company = await context.Companies.AsNoTracking()
                .Include(x => x.CountryFkNavigation)
                .Include(x => x.DefaultCultureFkNavigation)
                .ThenInclude(x => x.LanguageFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == companyId);
            companyDTO = company?.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyByIdAsync));
        }

        return companyDTO;
    }

    public async Task<CompanyDTO?> UpdateCompanyAsync(CompanyDTO newCompanyInfo)
    {
        CompanyDTO? savedRecord = null;

        try
        {
            var context = dataBaseContextFactory.GetDbContext();
            var companyDB = await context.Companies.FirstOrDefaultAsync(x => x.Id == newCompanyInfo.Id);
            if (companyDB is null)
            {
                return null;
            }

            var newCompanyDb = newCompanyInfo.ToCompany();
            context.Entry(companyDB).CurrentValues.SetValues(newCompanyDb);
            await context.SaveChangesAsync();
            savedRecord = companyDB.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteCompanyByIdAsync(long companyId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;


        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var companyToUpdate = await context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
            if (companyToUpdate is null)
            {
                return false;
            }

            var updatedCompany = companyToUpdate.CloneEntity();
            updatedCompany.IsActive = false;
            updatedCompany.LastUpdateByUserFk = deviceInformation.UserId;
            updatedCompany.LastUpdateDate = cultureRepository.UtcNow().DateTime;
            context.Entry(companyToUpdate).CurrentValues.SetValues(updatedCompany);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyAsync));
            throw;
        }

        return deleted;
    }

    public async Task<List<MembershipTypeDTO>> GetAllMembershipTypeListAsync(Expression<Func<MembershipType, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<MembershipTypeDTO> membershipTypeDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var membershipTypeList = await context.MembershipTypes.AsNoTracking()
                .Where(filters)
                .ToListAsync();
            membershipTypeDTOList = membershipTypeList.Select(x => x.ToMembershipTypeDTO()).ToList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllMembershipTypeListAsync));
        }

        return membershipTypeDTOList;
    }

    public async Task<MembershipTypeDTO?> GetMembershipTypeByIdAsync(long membershipTypeId, DataBaseServiceContext? context = null)
    {
        MembershipTypeDTO? membershipTypeDTO = null;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var membershipType = await context.MembershipTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == membershipTypeId);
            membershipTypeDTO = membershipType?.ToMembershipTypeDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetMembershipTypeByIdAsync));
        }

        return membershipTypeDTO;
    }

    public async Task<MembershipTypeDTO?> CreateNewMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        MembershipTypeDTO? savedRecord = null;
        var newRecordDb = membershipTypeDTO.ToMembershipType();
        newRecordDb.CreationDate = cultureRepository.UtcNow().DateTime;
        newRecordDb.CreatedByUserFk = deviceInformation.UserId;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.MembershipTypes.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToMembershipTypeDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewMembershipTypeAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<MembershipTypeDTO?> UpdateMembershipTypeByIdAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        MembershipTypeDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var membershipTypeToUpdate = await context.MembershipTypes.FirstOrDefaultAsync(x => x.Id == membershipTypeDTO.Id);
            if (membershipTypeToUpdate is null)
            {
                return null;
            }

            var updatedMembershipType = membershipTypeDTO.ToMembershipType();
            updatedMembershipType.LastUpdateDate = cultureRepository.UtcNow().DateTime;
            updatedMembershipType.LastUpdateByUserFk = deviceInformation.UserId;
            context.Entry(membershipTypeToUpdate).CurrentValues.SetValues(updatedMembershipType);
            await context.SaveChangesAsync();
            savedRecord = membershipTypeToUpdate.ToMembershipTypeDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateMembershipTypeByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteMembershipTypeByIdAsync(long membershipTypeId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var membershipType = await context.MembershipTypes.FirstOrDefaultAsync(x => x.Id == membershipTypeId);
            if (membershipType is null)
            {
                return false;
            }

            context.MembershipTypes.Remove(membershipType);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteMembershipTypeByIdAsync));
            throw;
        }

        return deleted;
    }

}
