using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Companies;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class CompaniesEndPoint
{
    public static void MapCompaniesEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Companies/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CompaniesFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("Companies/", GetAllCompanyListAsync)
   .MapToApiVersion(1.0)
   .WithSummary("Get All Company List Async")
     .WithDescription("[Author] [Authen] [Trace]")
   .RequireAuthorization()
   .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Companies/Add", CreateNewCompanyAsync)
            .MapToApiVersion(1.0)
           .WithSummary("Create New Company Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Companies/Memberships", GetCompanyMemberhipAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Memberhip By Company Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("CompanyApplication/update", UpdateCompanyApplicationsAsync)
     .MapToApiVersion(1.0)
     .WithSummary("Update Company Applications Async")
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
        var returnObject = await companiesService.GetCompanyMemberhipAsync(companyId ?? deviceInformation!.CompanyId!.Value);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CompanyDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCompanyListAsync(
        HttpRequest request,
        [FromServices] ICompaniesService  companiesService)

    {
        var returnObject = await companiesService.GetAllCompanyListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCompanyAsync(
       HttpRequest request,
       [FromServices] ICompaniesService  companiesService,
       [FromServices] IValidator<CompanyApplicationsDTO> validator,
       [FromBody] CompanyApplicationsDTO companyApplications,
       [FromQuery] long companyId,
       [FromQuery] long userId)
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


    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCompanyApplicationsAsync(
        HttpRequest request,
        [FromServices] ICompaniesService  companiesService,
        [FromServices] IValidator<CompanyApplicationsDTO> validator,
        [FromBody] CompanyApplicationsDTO companyApplicationInfo,
        [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyApplicationInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await companiesService.UpdateCompanyApplicationsAsync(companyApplicationInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

}
