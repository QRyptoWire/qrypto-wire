using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using QRyptoWire.App.WPhone.Models;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.CustomExceptions;
using QRyptoWire.Core.ModelsAbstraction;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
    class StorageService : IStorageService
    {
        public bool ArePushNotificationsEnabled()
        {
            return (bool)IsolatedStorageSettings.ApplicationSettings["PushNotifications"];
        }

        public void EnablePushNotifications()
        {
            IsolatedStorageSettings.ApplicationSettings["PushNotifications"] = true;
        }

        public void DisablePushNotifications()
        {
            IsolatedStorageSettings.ApplicationSettings["PushNotifications"] = false;
        }

        public void CreateUser(IUserItem user)
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                if (user == null || db.Users.Count() == 1) return;

                if (user.KeyPair == null)
                {
                    throw new ArgumentException("KeyPair can't be null");
                }

                db.Users.InsertOnSubmit(new UserItem()
                {
                    Id = user.Id,
                    KeyPair = user.KeyPair
                });

                db.SubmitChanges();
            }
        }

        public IUserItem GetUser()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                IUserItem user = db.Users.FirstOrDefault();

                if (user == null)
                {
                    throw new UserNotFoundException("User does not exist");
                }

                return user;
            }
        }

        public bool UserExists()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                bool userExists = db.Users.FirstOrDefault() != null;
                return userExists;
            }
        }

        public string GetKeyPair()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                IUserItem user = db.Users.FirstOrDefault();

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
                IUserItem user = db.Users.FirstOrDefault();

                if (user == null)
                {
                    throw new UserNotFoundException("User does not exist");
                }

                return user.Id;
            }
        }

        public IEnumerable<IContactItem> GetContacts()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                List<ContactItem> contacts = db.Contacts.ToList();
                return contacts;
            }
        }

        public IEnumerable<IMessageItem> GetMessages()
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                List<MessageItem> messages = db.Messages.ToList();
                return messages;
            }
        }

        public IEnumerable<IMessageItem> GetConversation(int contactId)
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                List<MessageItem> messages = db.Messages.Where(msg => msg.SenderId == contactId || msg.ReceiverId == contactId).ToList();
                return messages;
            }
        }

        public void AddContacts(IEnumerable<IContactItem> contacts)
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                if (contacts == null) return;

                List<ContactItem> contactsToAdd = contacts.Select(c => new ContactItem()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PublicKey = c.PublicKey,
                    IsNew = true
                }).ToList();

                db.Contacts.InsertAllOnSubmit(contactsToAdd);
                db.SubmitChanges();
            }
        }

        public void AddMessages(IEnumerable<IMessageItem> messages)
        {
            using (QRyptoDb db = QryptoDbFactory.GetDb())
            {
                if (messages == null)
                    return;

                List<MessageItem> messagesToAdd = messages.Select(m => new MessageItem()
                {
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    IsNew = true,
                    Body = m.Body,
                    Date = m.Date
                }).ToList();

                db.Messages.InsertAllOnSubmit(messagesToAdd);
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
    }
}
