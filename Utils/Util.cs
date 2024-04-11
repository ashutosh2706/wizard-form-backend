using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;

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


        public static string ReplaceSpecialChars(string fileName)
        {
            Regex reg = new("[*'\",&#^@\\/:?\"<>|]");
            return reg.Replace(fileName, string.Empty);
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
