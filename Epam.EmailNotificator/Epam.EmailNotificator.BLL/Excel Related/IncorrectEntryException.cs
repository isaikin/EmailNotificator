using System;
using System.Runtime.Serialization;

namespace Epam.EmailNotificator.BLL.Excel_Related
{
	[Serializable]
	public class IncorrectEntryException : IncorrectFileException
	{
		public IncorrectEntryException()
		{
		}

		public IncorrectEntryException(string message) : base(message)
		{
		}

		public IncorrectEntryException(string message, Exception inner) : base(message, inner)
		{
		}

		protected IncorrectEntryException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}