using System;
using Epam.Email.Notificator.Entities;
using System.Linq;	
using Epam.EmailNotificator.BLL.Excel_Related;

namespace Epam.EmailNotificator.BLL
{
    public class EmailNotificatorLogic
    {
        public void ParseFile(AppFile file)
        {
	        try
	        {
				using (var parser = new ExcelMessageAdapter(file.Data))
				{
					var messages = parser.ParseMessages().ToArray();

					// TODO: convert messages to dtos and so on

					//Save to db
				}
			}
	        catch (IncorrectEntryException ex)
	        {
				// file is correct, but data in a cel isn't
		        throw;
	        }
	        catch (IncorrectFileException ex)
	        {
				// wrong file
		        throw;
	        }
        }
    }
}
