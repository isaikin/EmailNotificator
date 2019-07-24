using System;
using System.Runtime.Serialization;

namespace Epam.EmailNotificator.BLL.Excel_Related
{
	[Serializable]
	public class IncorrectFileException : Exception
	{
		public IncorrectFileException()
		{
		}

		public IncorrectFileException(string message) : base(message)
		{
		}

		public IncorrectFileException(string message, Exception inner) : base(message, inner)
		{
		}

		protected IncorrectFileException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}