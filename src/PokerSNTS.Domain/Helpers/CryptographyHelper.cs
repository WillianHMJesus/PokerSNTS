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
            var hashs = sha256Managed.ComputeHash(Encoding.ASCII.GetBytes(text));

            foreach (var hash in hashs)
            {
                encryptedText += hash.ToString("x2");
            }

            return encryptedText;
        }
    }
}
