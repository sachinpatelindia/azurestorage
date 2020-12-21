namespace SKPNet.Security.DataValidation
{
    public interface IEncryptor
    {
        string CreateSaltKey(int size);
        string CreatePasswordHash(string password, string saltKey, string passwordFormat);
        string EncryptText(string palainText, string encryptionPrivateKey="");
        string DecryptText(string cipher, string encryptionPrivateKey="");

    }
}
