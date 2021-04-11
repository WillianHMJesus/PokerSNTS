using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PokerSNTS.Domain.Helpers
{
    public class CryptographyHelper
    {
        public static string Sha256(string text)
        {
            var encryptedText = string.Empty;
            var sha256Managed = new SHA256Managed();
            var hash = sha256Managed.ComputeHash(Encoding.ASCII.GetBytes(text));
            hash.Select(x => encryptedText += x.ToString("x2"));

            return encryptedText;
        }
    }
}
