using System;
using System.Linq;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.ModelsAbstraction;
using QRyptoWire.Core.Objects;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IStorageService _storageService;

        public EncryptionService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public EncryptedMessage Encrypt(string messageText, int recieverId)
        {
            if (messageText == null)
            {
                throw new ArgumentException("Message text cannot be null");
            }

            IContactModel receiverContact = _storageService.GetContacts().FirstOrDefault(c => c.Id == recieverId);        
            if (receiverContact == null)
            {
                throw new ArgumentException("Contact with id of receiverId does not exist");
            }

            EncryptedMessage encMsg = new EncryptedMessage();
            AesCipher aes = new AesCipher();

            // set initiazlization vector
            encMsg.IV = FormatConverter.BytesToString64(aes.IV); 

            // enccrypt message text symmetrically
            byte[] messageBytes = FormatConverter.StringToBytes(messageText);
            byte[] encryptedMessageBytes = aes.Encrypt(messageBytes);
            encMsg.Body = FormatConverter.BytesToString64(encryptedMessageBytes);

            // encrypt symmetric key with receivers public key
            string receiverPublicKey = receiverContact.PublicKey;
            RsaCipher rsa = new RsaCipher(receiverPublicKey);
            byte[] encryptedSymmetricKeyBytes = rsa.Encrypt(aes.Key);
            encMsg.SymmetricKey = FormatConverter.BytesToString64(encryptedSymmetricKeyBytes);

            // create digital signature of message text (using senders private key)
            string senderKeyPair = _storageService.GetUser().KeyPair;
            rsa = new RsaCipher(senderKeyPair);
            byte[] digitalSignatureBytes = rsa.CreateDigitalSignature(messageBytes);
            encMsg.DigitalSignature = FormatConverter.BytesToString64(digitalSignatureBytes);

            return encMsg;
        }

        public bool Decrypt(EncryptedMessage encryptedMessage, int senderId, out string messageText)
        {
            if (encryptedMessage == null)
            {
                throw new ArgumentException("Encrypted message cannot be null");
            }

            if (encryptedMessage.Body == null || encryptedMessage.DigitalSignature == null || encryptedMessage.SymmetricKey == null || encryptedMessage.IV == null)
            {
                throw new ArgumentException("Not all encrypted message fields are initialized");
            }

            IContactModel senderContact = _storageService.GetContacts().FirstOrDefault(c => c.Id == senderId);
            if (senderContact == null)
            {
                throw new ArgumentException("Contact with id of senderId does not exist");
            }

            string receiverKeyPair = _storageService.GetUser().KeyPair;
            string senderPublicKey = senderContact.PublicKey;

            try
            {
                // decrypt symmetric key with receivers private key
                RsaCipher rsa = new RsaCipher(receiverKeyPair);
                byte[] encryptedSymmetricKeyBytes = FormatConverter.String64ToBytes(encryptedMessage.SymmetricKey);
                byte[] decryptedSymmetricKeyBytes = rsa.Decrypt(encryptedSymmetricKeyBytes);

                // decrypt message text with jsut decrypted symmetric key
                byte[] ivBytes = FormatConverter.String64ToBytes(encryptedMessage.IV);
                AesCipher aes = new AesCipher(decryptedSymmetricKeyBytes, ivBytes);
                byte[] encryptedMessageBytes = FormatConverter.String64ToBytes(encryptedMessage.Body);
                byte[] decryptedMessageBytes = aes.Decrypt(encryptedMessageBytes);

                // set message text out parameter
                messageText = FormatConverter.BytesToString(decryptedMessageBytes);

                // verify digital signature
                rsa = new RsaCipher(senderPublicKey);
                byte[] digitalSignatureBytes = FormatConverter.String64ToBytes(encryptedMessage.DigitalSignature);
                bool signatureOk = rsa.VerifyDigitalSignature(decryptedMessageBytes, digitalSignatureBytes);

                return signatureOk;
            }
            catch (Exception ex)
            {
                messageText = null;
                return false;
            }
        }

        public bool ComposePublicKey(string modulus, string exponent, out string publicKey)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> DecomposePublicKey(string publicKey)
        {
            throw new NotImplementedException();
        }

        public string ExtractPublicKey(string keyPair)
        {
            throw new NotImplementedException();
        }

        public string GetKeyPair()
        {
            throw new NotImplementedException();
        }
    }
}
