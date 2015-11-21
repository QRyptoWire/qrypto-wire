using System.Collections.Generic;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
        bool ArePushNotificationsEnabled();
        void EnablePushNotifications();
        void DisablePushNotifications();
        void CreateUser(IUserItem user);
        IUserItem GetUser();
        bool UserExists();
        string GetKeyPair();
        int GetUserId();
        IEnumerable<IContactItem> GetContacts();
        IEnumerable<IMessageItem> GetMessages();
        IEnumerable<IMessageItem> GetConversation(int contactId);
        void AddContacts(IEnumerable<IContactItem> contacts);
        void AddMessages(IEnumerable<IMessageItem> messages);
        void ClearMessages();
    }
}
