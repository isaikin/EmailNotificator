using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Epam.EmailNotificator.BLL.Excel_Related
{
	public class ExcelMessageInfo
	{
		private const string EmailPattern = @"^(?:\w+[.])*\w+@\w+\.\w+$";

		public string Text { get; set; }
		public string EmailTo { get; set; }
		public string Subject { get; set; }
		public DateTime DateToSend { get; set; }
		public IEnumerable<string> EmailsCopyTo { get; set; }

		public void ParseEmail(string value)
		{
			EmailTo = IsEmailCorrect(value) ? value : throw new ArgumentException("value is not a correct email");
		}

		public void ParseText(string value)
		{
			Text = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("value is empty");
		}

		public void ParseSubject(string value)
		{
			Subject = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("value is empty");
		}

		public void ParseDate(DateTime value)
		{
			DateToSend = value > DateTime.Now ? value : throw new ArgumentException("Date has passed");
		}

		public void ParseCopyEmails(string value)
		{
			var emails = value.Trim().Split(new []{',', ';'}, StringSplitOptions.RemoveEmptyEntries)
				.Select(email => email.Trim())
				.ToArray();

			if (emails.Any(email => !IsEmailCorrect(email)))
			{
				throw new ArgumentException("list of copy emails contains an incorrect email");
			}

			EmailsCopyTo = emails;
		}


		public static bool IsEmailCorrect(string value)
		{
			return Regex.IsMatch(value, EmailPattern);
		}
	}
}