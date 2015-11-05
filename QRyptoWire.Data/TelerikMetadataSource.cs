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

			//config mapping
			var messageMapping = new MappingConfiguration<Message>();
			messageMapping
			.MapType(message => new
			{
				ID = message.Id,
				message.Content,
				message.SenderId,
				message.RecipientId
			}).ToTable("Messages");
			messageMapping
			.HasProperty(e => e.Id)
			.IsIdentity(KeyGenerator.Autoinc);

			var userMapping = new MappingConfiguration<User>();
			userMapping
			.MapType(user => new
			{
				ID = user.Id,
				user.PasswordHash,
				user.AllowPush,
				user.DeviceId
			}).ToTable("Users");
			userMapping
			.HasProperty(e => e.Id)
			.IsIdentity(KeyGenerator.Autoinc);

			var sessionMapping = new MappingConfiguration<Session>();
			sessionMapping
			.MapType(session => new
			{
				session.UserId,
				session.SessionKey,
			}).ToTable("Sessions");

			var contactMapping = new MappingConfiguration<Contact>();
			contactMapping
			.MapType(contact => new
			{
				ID = contact.Id,
				contact.Content,
				SenderId = contact.Sender,
				RecipientId = contact.Recipient
			}).ToTable("Contacts");

			//set relations
			messageMapping
			.HasAssociation(e => e.Sender)
				.ToColumn("SenderId")
				.HasConstraint((m, u) => m.SenderId == u.Id);

			//set relations
			messageMapping
			.HasAssociation(e => e.Recipient)
				.ToColumn("RecipientId")
				.WithOpposite(u => u.ReceivedMessages)
				.HasConstraint((m, u) => m.RecipientId == u.Id);

			sessionMapping
				.HasAssociation(s => s.User)
				.ToColumn("UserId");

			//add configs
			configurations.Add(messageMapping);
			configurations.Add(userMapping);
			configurations.Add(sessionMapping);
			configurations.Add(contactMapping);

			return configurations;
		}
	}
}
