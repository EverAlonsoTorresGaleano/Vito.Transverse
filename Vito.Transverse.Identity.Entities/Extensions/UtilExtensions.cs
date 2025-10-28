using System.Reflection;
using Vito.Framework.Common.Extensions;
using Vito.Transverse.Identity.Entities.Constants;

namespace Vito.Transverse.Identity.Entities.Extensions;

public static class UtilExtensions
{
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
        catch
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
        catch
        {

        }
        return cadenaSalida;
    }



}
