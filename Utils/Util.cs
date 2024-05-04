using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace WizardFormBackend.Utils
{
    public static class Util
    {
        public static string GenerateHash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }


        public static string Sanitize(string query)
        {
            query = ReplaceSpecialChars(query);
            string[] reservedKeywords = { "SELECT", "UPDATE", "DELETE", "INSERT", "ALTER", "DROP", "CREATE", "TRUNCATE", "RENAME", "JOIN", "INNER", "OUTER", "LEFT", "RIGHT", "WHERE", "FROM", "INTO", "SET", "VALUES", "ORDER", "BY", "GROUP", "HAVING", "LIMIT", "OFFSET", "TOP", "DISTINCT", "UNION", "ALL" };
            foreach (var reservedKeyword in reservedKeywords)
            {
                string pattern = $@"\b{Regex.Escape(reservedKeyword)}\b";
                // Replace all occurrences of the reserved keyword with an underscore
                query = Regex.Replace(query, pattern, "_", RegexOptions.IgnoreCase);
            }
            return query;
        }


        public static string ReplaceSpecialChars(string input)
        {
            Regex reg = new("[*'\",&#^@\\/:?\"<>|]");
            return reg.Replace(input, string.Empty);
        }


        public static string CalculateChecksum(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

    }
}
