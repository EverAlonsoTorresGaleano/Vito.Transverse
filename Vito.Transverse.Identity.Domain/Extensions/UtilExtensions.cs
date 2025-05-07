using Azure;
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

    public static string ToStringEncrypted(this string sentence, bool criptografyEnabled)
    {
        string cadenaSalida = sentence;
        try
        {
            if (criptografyEnabled)
            {
                byte[] bytes;
                bytes = System.Text.Encoding.Unicode.GetBytes(sentence);
                cadenaSalida = Convert.ToBase64String(bytes);
            }
        }
        catch (Exception error)
        {

        }
        return cadenaSalida;
    }

    public static string ToStringDecrypted(this string encryptedSentence, bool criptografyEnabled)
    {
        string cadenaSalida = encryptedSentence;
        try
        {
            if (criptografyEnabled)
            {
                byte[] bytes;
                bytes = Convert.FromBase64String(encryptedSentence);
                cadenaSalida = System.Text.Encoding.Unicode.GetString(bytes, 0, bytes.ToArray().Length);
            }
        }
        catch (Exception error)
        {

        }
        return cadenaSalida;
    }

}
