using Microsoft.FeatureManagement;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Framework.Api.Filters;

namespace Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;

/// <summary>
/// Feature Flag for Master Api
/// </summary>
/// <param name="featureManager"></param>
public class MasterFeatureFlagFilter(IFeatureManager featureManager) : FeatureFlagFilter(featureManager)
{
    protected override string FeatureFlag => FeatureFlagsNamesEnum.MasterFeature.ToString();
}

