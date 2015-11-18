namespace QRyptoWire.Core.Services
{
    public interface IEncryptionService
    {
        bool IsLazyMotherfucker();
        string Encrypt(string message, int recieverUUID);
        string Decrypt(string message, int senderUUID);
        bool ComposePublicKey(string modulus, string exponent, out string publicKey);
    }
}