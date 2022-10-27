using System;

namespace Sat.Recruitment.Api.Core.Util
{
    public class TextFormater
    {
        public static string NomralizeEmail(string email) {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            var emailFormated = string.Join("@", new string[] { aux[0], aux[1] });

            return emailFormated;
        }
    }
}
