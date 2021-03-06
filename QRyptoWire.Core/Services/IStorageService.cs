﻿using System;
using System.Collections.Generic;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
        bool IsPushEnabled();
		void SetPushSettings(bool enable);
	    void SaveUser(UserItem user);
	    IUserModel GetUser();
        void SetUserName(string name);
        bool UserExists();
	    IEnumerable<IContactModel> GetContacts();
        IEnumerable<Tuple<IContactModel, int>> GetContactsWithNewMessageCount();
	    IEnumerable<IMessageModel> GetMessages(int contactId);
        void SaveContacts(IEnumerable<ContactItem> contacts);
        void SaveMessages(IEnumerable<MessageItem> messages);
        void ClearMessages();
	    void MarkContactsAsNotNew(IEnumerable<int> contactsId);
	}
}
