using System.Linq;
using Windows.Security.Credentials;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class QryptoDbSecurity
    {
        private const string UserNameText = "QryptoUser";
        private const string ResourceText = "QryptoDbPassword";
        private const int PasswordLength = 40;

        public static string GetPassword()
        {
            PasswordVault passwordVault = new PasswordVault();
            if (!passwordVault.RetrieveAll().Any(credential => credential.UserName == UserNameText && credential.Resource == ResourceText))
            {
                SetPassword();
            }

            return passwordVault.Retrieve(ResourceText, UserNameText).Password;
        }

        private static void SetPassword()
        {
            PasswordVault passwordVault = new PasswordVault();
            var credentials = new PasswordCredential()
            {
                UserName = UserNameText,
                Resource = ResourceText,
                Password = PasswordGenerator.Next(PasswordLength)

            };
            passwordVault.Add(credentials);
        }
    }
}
