using Epam.EmailNotificator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace EmailNotificator
{
    class Program
    {
        static void Main(string[] args)
        {
            //пользователи которым отправляется сообщение
            AppUser testAppUser1 = new AppUser();
            testAppUser1.Email = "yasyukn@mail.ru";
            AppUser testAppUser2 = new AppUser();
            testAppUser2.Email = "niknik1111nik@yandex.ru";
            AppUser[] arrayTo = new AppUser[2] { testAppUser1, testAppUser2 };

            //прикрепление файлов
            AttacheFile attacheFile = new AttacheFile();
            byte[] attacheFileBytes = File.ReadAllBytes("d://test_excel.xlsx");
            attacheFile.ArrayByte = attacheFileBytes;
            attacheFile.FileName = "test_excel.xlsx";
            List<AttacheFile> listAttachedFiles = new List<AttacheFile>();
            listAttachedFiles.Add(attacheFile);
            listAttachedFiles.Add(attacheFile);
            AppUser[] arrayCopy = new AppUser[1] { testAppUser1 };

            //создание объекта логики
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            Sender sender = new Sender("emailNotificator.app.2019@gmail.com", "12345qwert_", "EmailNotificator");
            SendMessageLogic sendMessageLogic = new SendMessageLogic(sender, smtp);

            //создание объекта отправки
            string subject = "TestNotificationSubject";
            string message = "TestNotificationMessage";
            SendMessage sendObject = new SendMessage(subject, message, listAttachedFiles, arrayTo, arrayCopy);

            sendMessageLogic.Send(sendObject);
            Console.ReadKey();
        }
    }
}
