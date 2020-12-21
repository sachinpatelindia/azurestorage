using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SKPNet.Security.DataValidation
{
    /// <summary>
    /// Encryptor and Decryptor
    /// </summary>
    public partial class Encryptor : IEncryptor
    {
        public string CreatePasswordHash(string password, string saltKey, string passwordFormat)
        {
            return CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)), passwordFormat);
        }

        public string CreateSaltKey(int size)
        {
            var provider = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            provider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public string DecryptText(string cipher, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(cipher))
                return cipher;
            if (string.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = "";
            var provider = new TripleDESCryptoServiceProvider
            {
                Key=Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0,16)),
                IV=Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0,8))
            };

            var buffer = Convert.FromBase64String(cipher);
            return DecryptTextFromMemory(buffer,provider.Key,provider.IV);
        }

        public string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;
            if (string.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = "";
            var provider = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0,8))
            };

            var encryptedBinary = EncryptTextFromMemory(plainText, provider.Key, provider.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        private byte[] EncryptTextFromMemory(string data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                var toEncrypt = Encoding.Unicode.GetBytes(data);
                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
            }
            return ms.ToArray();
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                var sr = new StreamReader(cs, Encoding.Unicode);
                return sr.ReadToEnd();
            }
        }

        private string CreateHash(byte[] data, string hashAlgorithm, int trimByteCount=0)
        {
            if (string.IsNullOrEmpty(hashAlgorithm))
                throw new ArgumentNullException(nameof(hashAlgorithm));
            var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentNullException(nameof(algorithm));
            if(trimByteCount>0 && data.Length>trimByteCount)
            {
                var newData = new byte[trimByteCount];
                Array.Copy(data, newData, trimByteCount);
                return BitConverter.ToString(algorithm.ComputeHash(newData)).Replace("-",string.Empty);
            }
            return BitConverter.ToString(algorithm.ComputeHash(data)).Replace("-",string.Empty);
        }
    }
}
