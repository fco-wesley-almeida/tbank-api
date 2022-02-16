using System;
using System.Security.Cryptography;
using System.Text;
using Core.CoreServices;

namespace Business.CoreServices
{
    public class PasswordEncoder: IPasswordEncoder
    {
        private static string KeyMd5 => "E(H+MbQeThWmZq4t7w!z%C&F)J@NcRfUjXn2r5u8x/A?D(G-KaPdSgVkYp3s6v9y";

        public string Encode(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KeyMd5));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = tdes.CreateEncryptor();
            byte[] textBytes = Encoding.UTF8.GetBytes(password);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        public string Decode(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KeyMd5));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = tdes.CreateDecryptor();

            byte[] cipherBytes = Convert.FromBase64String(password);
            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}