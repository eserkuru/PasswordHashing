using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "P@ssworD";
            var salt = CreateSalt(8);

            var hash = GenerateSaltedHash(
                Encoding.UTF8.GetBytes(password),
                Encoding.UTF8.GetBytes(salt));

            var result = Convert.ToBase64String(hash);
            Console.WriteLine("PasswordHash : {0}", Convert.ToBase64String(hash));
            Console.WriteLine("PasswordSalt : {0}", salt);
            Console.ReadLine();
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
    }
}
