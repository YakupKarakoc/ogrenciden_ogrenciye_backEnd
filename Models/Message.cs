using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Message
	{
		[Key]
		public int MessageId { get; set; }

		[Required]
		public int SenderId { get; set; } // Gönderen kullanıcı ID'si

		[Required]
		public int ReceiverId { get; set; } // Alıcı kullanıcı ID'si

		[Required]
		public string Content { get; set; } // Mesaj içeriği

		public DateTime SentAt { get; set; } = DateTime.UtcNow;

		[ForeignKey("SenderId")]
		public User Sender { get; set; } // Gönderen kullanıcı

		[ForeignKey("ReceiverId")]
		public User Receiver { get; set; } // Alıcı kullanıcı
	}
}
