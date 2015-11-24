using System;
using System.Collections.Generic;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
        bool ArePushNotificationsEnabled();
        void EnablePushNotifications();
        void DisablePushNotifications();
	    void SaveUser(UserItem user);
        IUserModel GetUser();
        bool UserExists();
        string GetKeyPair();
        int GetUserId();
        IEnumerable<IContactModel> GetContacts();
        IEnumerable<Tuple<IContactModel, int>> GetContactsWithNewMessageCount();
        IEnumerable<IMessageModel> GetMessages();
	    IEnumerable<IMessageModel> GetMessages(int contactId);
        void SaveContacts(IEnumerable<ContactItem> contacts);
        void SaveMessages(IEnumerable<MessageItem> messages);
        void ClearMessages();
	    void MarkMessagesAsNotNew(IEnumerable<int> messagesId);
	    void MarkContactsAsNotNew(IEnumerable<int> contactsId);
	}
}
