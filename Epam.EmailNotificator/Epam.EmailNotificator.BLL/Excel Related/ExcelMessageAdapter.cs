using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using Epam.Email.Notificator.Entities;
using OfficeOpenXml;

namespace Epam.EmailNotificator.BLL.Excel_Related
{
	public class ExcelMessageAdapter : IDisposable
	{
		private const string WorksheetName = "MessagesList";

		private readonly ExcelPackage _excel;
		private ExcelWorksheet _worksheet;
		private string _firstCell;

		/// <summary>
		///		Adapter for an excel library. Used to parse messages from file
		/// </summary>
		/// <exception cref="IncorrectFileException">
		///		thrown if data is not an excel file or it was created using wrong template
		/// </exception>
		/// <param name="file"></param>
		public ExcelMessageAdapter(byte[] file)
		{
			try
			{
				_excel = new ExcelPackage(new MemoryStream(file));
			}
			catch (Exception ex) // TODO: Find out which exception is thrown if stream is not an excel file
			{
				throw new IncorrectFileException("File is not an excel file", ex);
			}
			Init();
		}


		private void Init()
		{
			if (_excel.Workbook.Worksheets.All(worksheet => worksheet.Name != WorksheetName))
			{
				throw new IncorrectFileException($"Excel file doesn't contain worksheet \"{WorksheetName}\"");
			}

			_worksheet = _excel.Workbook.Worksheets[WorksheetName];

			_firstCell = _worksheet.Cells[1, 1].Value as string;
			if (_firstCell is null) throw new IncorrectFileException("Worksheet doesn't contain info about where list starts");
		}

		public IEnumerable<ExcelMessageInfo> ParseMessages()
		{
			var cell = new ExcelCellAddress(_firstCell);
			while (!(_worksheet.Cells[cell.Address].Value is null))
			{
				yield return ParseLine(cell);
				cell = new ExcelCellAddress(cell.Row+1, cell.Column);
			} 
		}

		private ExcelMessageInfo ParseLine(ExcelCellAddress cell)
		{
			var message = new ExcelMessageInfo();

			try
			{
				// 1. How to make the order configurable?
				message.ParseEmail     (GetMergedValue<string>   (cell.Row, cell.Column + 0));
				message.ParseText      (GetMergedValue<string>   (cell.Row, cell.Column + 1));
				message.ParseSubject   (GetMergedValue<string>   (cell.Row, cell.Column + 2));
				message.ParseDate      (GetMergedValue<DateTime> (cell.Row, cell.Column + 3));
				message.ParseCopyEmails(GetMergedValue<string>   (cell.Row, cell.Column + 4));
			}
			catch (ArgumentException ex)
			{
				// TODO: modify exception to make it include data
				throw new IncorrectEntryException($"Row {cell.Row} contains incorrect data", ex);
			}

			return message;
		}

		private T GetMergedValue<T>(int row, int column) => 
			GetMergedValue<T>(ExcelAddress.GetAddress(row, column));

		private T GetMergedValue<T>(string address)
		{
			var range = _worksheet.Cells[address];
			if (!range.Merge)
			{
				return _worksheet.Cells[range.Start.Address].GetValue<T>();
			}

			var mergedAddress = new ExcelAddress(_worksheet.MergedCells[range.Start.Row, range.Start.Column]);

			return _worksheet.Cells[mergedAddress.Start.Address].GetValue<T>();
		}

		public void Dispose()
		{
			_excel?.Dispose();
		}
	}
}