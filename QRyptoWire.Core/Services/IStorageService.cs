using System.Collections.Generic;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
		bool KeyPairExists();
        string GetKeyPair();
	    int GetUserId();
	    IEnumerable<IContactItem> GetContacts();
	    IEnumerable<IMessageItem> GetMessages();
	    IEnumerable<IMessageItem> GetMessages(int contactId);
	    void AddContacts(IEnumerable<IContactItem> contacts);
	    void AddMessages(IEnumerable<IMessageItem> messages);
        void ClearMessages();
    }
}
