using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Application.TransverseServices.Companies;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class CompaniesEndPoint
{
    public static void MapCompaniesEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Companies/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CompaniesFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("", GetAllCompanyListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Company List Async")
                .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("dropdown", GetAllCompanyListItemAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get All Company List Async")
               .WithDescription("[Author] [Authen] [Trace]")
           .AllowAnonymous();
           //.AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("{companyId}", GetCompanyByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("", CreateNewCompanyAsync)
            .MapToApiVersion(1.0)
           .WithSummary("Create New Company Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("", UpdateCompanyByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Company By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("{companyId}", DeleteCompanyByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Company By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Memberships", GetCompanyMemberhipAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Memberhip By Company Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Memberships/dropdown", GetCompanyMemberhipListItemAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get Company Memberhip By Company Async")
           .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Memberships/{membershipId}", GetCompanyMembershipByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Membership By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Memberships", UpdateCompanyMembershipByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Company Membership By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Memberships/{membershipId}", DeleteCompanyMembershipByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Company Membership By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("MembershipTypes", GetAllMembershipTypeListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Membership Type List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("MembershipTypes/dropdown", GetAllMembershipTypeListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Membership Type List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("MembershipTypes/{membershipTypeId}", GetMembershipTypeByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Membership Type By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("MembershipTypes", CreateNewMembershipTypeAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Membership Type Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("MembershipTypes", UpdateMembershipTypeByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Membership Type By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("MembershipTypes/{membershipTypeId}", DeleteMembershipTypeByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Membership Type By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();
    }


    public static async Task<Results<Ok<List<CompanyMembershipsDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyMemberhipAsync(
       HttpRequest request,
       [FromServices] ICompaniesService companiesService,
       [FromQuery] long? companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await companiesService.GetCompanyMemberhipAsync(companyId ?? deviceInformation!.CompanyId!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyMemberhipListItemAsync(
   HttpRequest request,
   [FromServices] ICompaniesService companiesService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await companiesService.GetCompanyMemberhipListItemAsync(deviceInformation!.CompanyId!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<CompanyDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCompanyListAsync(
    HttpRequest request,
    [FromServices] ICompaniesService companiesService)

    {
        var returnObject = await companiesService.GetAllCompanyListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCompanyListItemAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await companiesService.GetAllCompanyListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    

    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCompanyAsync(
       HttpRequest request,
       [FromServices] ICompaniesService  companiesService,
       [FromServices] IValidator<CompanyDTO> validator,
       [FromBody] CompanyDTO companyApplications)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyApplications);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.CreateNewCompanyAsync(companyApplications, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }




    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService  companiesService,
        [FromRoute] long companyId)

    {
        var returnObject = await companiesService.GetCompanyByIdAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCompanyByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService  companiesService,
        [FromServices] IValidator<CompanyDTO> validator,
        [FromBody] CompanyDTO companyInfo)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(companyInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.UpdateCompanyByIdAsync(companyInfo.Id, companyInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCompanyByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService  companiesService,
        [FromRoute] long companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await companiesService.DeleteCompanyByIdAsync(companyId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<CompanyMembershipsDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyMembershipByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromRoute] long membershipId)
    {
        var returnObject = await companiesService.GetCompanyMembershipByIdAsync(membershipId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyMembershipsDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCompanyMembershipByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromServices] IValidator<CompanyMembershipsDTO> validator,
        [FromBody] CompanyMembershipsDTO membershipInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(membershipInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.UpdateCompanyMembershipByIdAsync(membershipInfo.Id, membershipInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCompanyMembershipByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromRoute] long membershipId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await companiesService.DeleteCompanyMembershipByIdAsync(membershipId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<List<MembershipTypeDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllMembershipTypeListAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService)
    {
        var returnObject = await companiesService.GetAllMembershipTypeListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllMembershipTypeListItemAsync(
    HttpRequest request,
    [FromServices] ICompaniesService companiesService)
    {
        var returnObject = await companiesService.GetAllMembershipTypeListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<MembershipTypeDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetMembershipTypeByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromRoute] long membershipTypeId)
    {
        var returnObject = await companiesService.GetMembershipTypeByIdAsync(membershipTypeId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<MembershipTypeDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewMembershipTypeAsync(
       HttpRequest request,
       [FromServices] ICompaniesService companiesService,
       [FromServices] IValidator<MembershipTypeDTO> validator,
       [FromBody] MembershipTypeDTO membershipTypeDTO,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(membershipTypeDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.CreateNewMembershipTypeAsync(membershipTypeDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<MembershipTypeDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateMembershipTypeByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromServices] IValidator<MembershipTypeDTO> validator,
        [FromBody] MembershipTypeDTO membershipTypeDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(membershipTypeDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.UpdateMembershipTypeByIdAsync(membershipTypeDTO.Id, membershipTypeDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteMembershipTypeByIdAsync(
        HttpRequest request,
        [FromServices] ICompaniesService companiesService,
        [FromRoute] long membershipTypeId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await companiesService.DeleteMembershipTypeByIdAsync(membershipTypeId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

}
