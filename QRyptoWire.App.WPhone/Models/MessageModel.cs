﻿using System;
using System.Data.Linq.Mapping;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.App.WPhone.Models
{
    [Table(Name = "Message")]
    public class MessageModel : IMessageModel
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int Id { get; set; }

        [Column(CanBeNull = true)]
        public int SenderId { get; set; }

        [Column(CanBeNull = true)]
        public int ReceiverId { get; set; }

        [Column(CanBeNull = false)]
        public string Body { get; set; }

        [Column(CanBeNull = false)]
        public bool IsNew { get; set; }

        [Column(CanBeNull = false)]
        public DateTime Date { get; set; }
    }
}
