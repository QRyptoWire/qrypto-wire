using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace QRyptoWire.Service.Data
{
	class TelerikMetadataSource : FluentMetadataSource
	{
		protected override IList<MappingConfiguration> PrepareMapping()
		{
			var configurations = 
				new List<MappingConfiguration>();

			var messageMapping = new MappingConfiguration<Message>();
			messageMapping.MapType(message => new
			{
				ID = message.Id,
				message.Content,
				SenderId = message.Sender.Id,
				RecipientId = message.Recipient.Id
			}).ToTable("Messages");
			messageMapping.HasProperty(e => e.Id).IsIdentity(KeyGenerator.Autoinc);
			messageMapping.HasAssociation(e => e.Sender)
				.ToColumn("SenderId")
				.WithOpposite(u => u.ReceivedMessages)
				.HasConstraint((m, u) => m.Sender.Id == u.Id);

			configurations.Add(messageMapping);

			var userMapping = new MappingConfiguration<User>();
			userMapping.MapType(user => new
			{
				ID = user.Id,
				user.PasswordHash,
				user.AllowPush
			}).ToTable("Users");

			configurations.Add(userMapping);

			var sessionMapping = new MappingConfiguration<Session>();
			sessionMapping.MapType(session => new
			{
				UserId = session.User,
				session.SessionKey
			}).ToTable("Sessions");

			configurations.Add(sessionMapping);

			var contactMapping = new MappingConfiguration<Contact>();
			contactMapping.MapType(contact => new
			{
				ID = contact.Id,
				contact.Content,
				SenderId = contact.Sender,
				RecipientId = contact.Recipient
			}).ToTable("Contacts");

			configurations.Add(contactMapping);

			return configurations;
		}
	}
}
