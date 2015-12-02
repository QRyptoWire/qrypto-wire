using QRyptoWire.Core.Objects;

namespace QRyptoWire.Core.Services
{
    public interface IEncryptionService
    {
        EncryptedMessage Encrypt(string messageText, int recieverId);
        bool Decrypt(EncryptedMessage encryptedMessage, int senderId, out string messageText);
        string GenerateKeyPair();
    }
}