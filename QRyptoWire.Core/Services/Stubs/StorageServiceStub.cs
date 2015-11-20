using System.Collections.Generic;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.Core.Services.Stubs
{
	public class StorageServiceStub : IStorageService
	{
		public bool KeyPairExists()
		{
			return true;
		}

	    public string GetKeyPair()
	    {
	        throw new System.NotImplementedException();
	    }

	    public int GetUserId()
	    {
	        return 0;
	    }

	    public IEnumerable<IContactItem> GetContacts()
	    {
	        throw new System.NotImplementedException();
	    }

	    public IEnumerable<IMessageItem> GetMessages()
	    {
	        throw new System.NotImplementedException();
	    }

	    public IEnumerable<IMessageItem> GetMessages(int contactId)
	    {
	        throw new System.NotImplementedException();
	    }

	    public void AddContacts(IEnumerable<IContactItem> contacts)
	    {
	        throw new System.NotImplementedException();
	    }

	    public void AddMessages(IEnumerable<IMessageItem> messages)
	    {
	        throw new System.NotImplementedException();
	    }

	    public void ClearMessages()
		{
		}
	}
}
