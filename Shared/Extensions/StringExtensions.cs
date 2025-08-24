using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Normaliza títulos para comparação:
        /// - lowercase
        /// - remove acentos (ex: é -> e, ç -> c)
        /// - substitui ":" por espaço
        /// - normaliza apóstrofos
        /// - trim
        /// </summary>
        public static string NormalizeTitle(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.ToLowerInvariant();
            input = input.Replace("’", "'"); // normaliza apóstrofos
            input = input.Replace(":", " "); // trata ":" como espaço
            input = input.Trim();

            // remove acentos/diacríticos
            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
