namespace QRyptoWire.Core.Objects
{
    public class EncryptedMessage
    {
        public string Body { get; set; } // encrypted with symmetric key using given IV
        public string DigitalSignature { get; set; } // digital signature of plaintext body (sha1 hash of plaintext body encrypted with senders private key)
        public string SymmetricKey { get; set; } // aes 256-bit symmetric key encrypted with receivers public key (rsa)
        public string IV { get; set; } // aes initialization vector
    }
}
