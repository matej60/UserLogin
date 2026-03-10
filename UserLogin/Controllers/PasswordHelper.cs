using System.Security.Cryptography;
using System.Text;

namespace UserLogin.Controllers
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in data)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
