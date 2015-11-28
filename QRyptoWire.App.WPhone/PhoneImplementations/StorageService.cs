using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO.IsolatedStorage;
using System.Linq;
using QRyptoWire.App.WPhone.Models;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.CustomExceptions;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.ModelsAbstraction;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
    class StorageService : IStorageService
    {
        public bool IsPushEnabled()
        {
	        if (!IsolatedStorageSettings.ApplicationSettings.Contains("PushNotifications"))
		        IsolatedStorageSettings.ApplicationSettings["PushNotifications"] = true;
            return (bool)IsolatedStorageSettings.ApplicationSettings["PushNotifications"];
        }

        public void SetPushSettings(bool enable)
        {
	        IsolatedStorageSettings.ApplicationSettings["PushNotifications"] = enable;
        }

        public void SaveUser(UserItem user)
        {
            if (user == null)
            {
                throw new ArgumentException("User can't be null");
            }
            if (user.KeyPair == null)
            {
                throw new ArgumentException("KeyPair can't be null");
            }

            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                if (db.Users.Count() == 1) return;

                db.Users.InsertOnSubmit(new UserModel()
                {
                    Id = user.Id,
                    KeyPair = user.KeyPair
                });

                db.SubmitChanges();
            }
        }

        public bool UserExists()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                bool userExists = db.Users.Any();
                return userExists;
            }
        }

        public string GetKeyPair()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                UserModel user = db.Users.FirstOrDefault();

                if (user == null)
                {
                    throw new UserNotFoundException("User does not exist");
                }

                return user.KeyPair;
            }
        }

        public int GetUserId()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                UserModel user = db.Users.FirstOrDefault();

                if (user == null)
                {
                    throw new UserNotFoundException("User does not exist");
                }

                return user.Id;
            }
        }

        public IEnumerable<Tuple<IContactModel, int>> GetContactsWithNewMessageCount()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                return db.Contacts.Select(
                        c =>
                            new Tuple<IContactModel, int>(c,
                                c.MessagesSentToUser.Count(mm => mm.IsNew) + c.MessagesSentByUser.Count(mm => mm.IsNew)))
                        .ToList();
            }
        }

        public IEnumerable<IMessageModel> GetMessages(int contactId)
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                ContactModel contact = db.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    throw new ArgumentException("Contact does not exist");
                }

                List<MessageModel> messages = contact.MessagesSentByUser.ToList();
                messages.AddRange(contact.MessagesSentToUser);

	            foreach (MessageModel msg in messages)
	            {
		            msg.IsNew = false;
	            }
				db.SubmitChanges();

                return messages;
            }
        }

        public void SaveContacts(IEnumerable<ContactItem> contacts)
        {
            if (contacts == null) return;

            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                db.Contacts.InsertAllOnSubmit(contacts.Select(c => new ContactModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PublicKey = c.PublicKey,
                    IsNew = c.IsNew,
                    MessagesSentByUser = new EntitySet<MessageModel>(),
                    MessagesSentToUser = new EntitySet<MessageModel>()
                }));
                db.SubmitChanges();
            }
        }

        public void SaveMessages(IEnumerable<MessageItem> messages)
        {
            if (messages == null) return;

            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                db.Messages.InsertAllOnSubmit(messages.Select(m => new MessageModel()
                {
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    IsNew = m.IsNew,
                    Body = m.Body,
                    Date = m.Date
                }));
                db.SubmitChanges();
            }
        }

        public void ClearMessages()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                db.Messages.DeleteAllOnSubmit(db.Messages);
                db.SubmitChanges();
            }
        }

        public void MarkContactsAsNotNew(IEnumerable<int> contactsId)
        {
            if (contactsId == null) return;

            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                foreach (var contact in db.Contacts.Where(c => contactsId.Contains(c.Id)))
                {
                    contact.IsNew = false;
                }
                db.SubmitChanges();
            }
        }
    }
}
