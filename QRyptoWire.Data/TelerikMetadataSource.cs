using System;
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
				message.RecipientId,
				message.Signature,
				message.SessionKey,
				message.SentTime
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
				user.PushToken,
				user.DeviceId,
				user.AllowPush
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
				session.StarTime
			}).ToTable("Sessions");

			var dateTimePropertyConfig = sessionMapping.HasProperty(s=> s.StarTime);
			dateTimePropertyConfig.IsCalculatedOn(DateTimeAutosetMode.InsertAndUpdate);

			var contactMapping = new MappingConfiguration<Contact>();
			contactMapping
			.MapType(contact => new
			{
				ID = contact.Id,
				contact.Name,
				contact.PublicKey,
				contact.SenderId,
				contact.RecipientId
			}).ToTable("Contacts");

			//set relations
			contactMapping
			.HasAssociation(e => e.Sender)
				.ToColumn("SenderId")
				.HasConstraint((m, u) => m.SenderId == u.Id);
			contactMapping
			.HasAssociation(e => e.Recipient)
				.ToColumn("RecipientId")
				.WithOpposite(u => u.ReceivedContacts)
				.HasConstraint((m, u) => m.RecipientId == u.Id);
				
			messageMapping
			.HasAssociation(e => e.Sender)
				.ToColumn("SenderId")
				.HasConstraint((m, u) => m.SenderId == u.Id);
			messageMapping
			.HasAssociation(e => e.Recipient)
				.ToColumn("RecipientId")
				.WithOpposite(u => u.ReceivedMessages)
				.HasConstraint((m, u) => m.RecipientId == u.Id);

			sessionMapping
				.HasAssociation(s => s.User)
				.ToColumn("UserId")
				.HasConstraint((m, u) => m.UserId == u.Id);

			//add configs
			configurations.Add(messageMapping);
			configurations.Add(userMapping);
			configurations.Add(sessionMapping);
			configurations.Add(contactMapping);

			return configurations;
		}
	}
}
