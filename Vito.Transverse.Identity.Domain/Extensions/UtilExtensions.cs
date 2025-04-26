using System.Text;

namespace Vito.Transverse.Identity.Domain.Extensions;

public static class UtilExtensions
{
    public static string ReplaceParameterOnString(this string? baseString, List<KeyValuePair<string, string>> parameters, string paramPrefix = "{{", string paramSufix = "}}")
    {
        StringBuilder finalString = new(baseString);
        if (parameters is not null)
        {
            parameters.ForEach(parameter =>
            {
                finalString = finalString.Replace($"{paramPrefix}{parameter.Key}{paramSufix}", parameter.Value);
            });
        }
        return finalString.ToString();
    }

}
