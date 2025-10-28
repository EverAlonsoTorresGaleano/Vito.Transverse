using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Security;
using Vito.Transverse.Identity.Entities.DTO;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;

public class CompaniesRepository(ILogger<SecurityRepository> logger, IDataBaseContextFactory dataBaseContextFactory) : ICompaniesRepository
{


    public async Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyDTO? savedRecord = null;
        var newRecordDb = newRecord.Company.ToCompany();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
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

    public async Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Companies.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Company.Id);
            recordToUpdateDb = recordToUpdate.Company.ToCompany(); ;
            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb!.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyMembershipsDTO> applicationDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyMembershipsList = await context.CompanyMemberships
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
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

    public async Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyDTO> companyDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyList = await context.Companies

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


}
