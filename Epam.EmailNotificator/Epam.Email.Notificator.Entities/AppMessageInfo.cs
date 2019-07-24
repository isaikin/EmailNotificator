using System;

namespace Epam.Email.Notificator.Entities
{
	public class AppMessageInfo
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public string EmailTo { get; set; }
		public string Subject { get; set; }
		public DateTime DateToSend { get; set; }
	}
}