using Epam.Email.Notificator.Entities;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using EmailNotificator;

namespace Epam.EmailNotificator
{
    class SendMessageLogic
    {
        public Sender MailSender { get; set; }
        public SmtpClient Smtp { get; set; }
        public SendMessageLogic(Sender sender, SmtpClient smtp)
        {
            MailSender = sender;
            Smtp = smtp;
        }

        public void Send(SendMessage sendMessage)
        {
            //отправитель - адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(MailSender.Email, MailSender.Name);

            //объект сообщения
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = from;

            //тема письма
            mailMessage.Subject = sendMessage.Subject;

            //текст письма
            mailMessage.Body = sendMessage.Message;

            //письмо представляет  собой код html
            mailMessage.IsBodyHtml = true;

            //добавление прикрепляемых файлов
            foreach (AttacheFile i in sendMessage.AttacheFiles)
            {
                MemoryStream file = new MemoryStream(i.ArrayByte, false);
                Attachment attachment = new Attachment(file, i.FileName);
                mailMessage.Attachments.Add(attachment);
            }

            //адрес smtp-сервера и порт для отправки письма
            using (Smtp)
            {
                //логин и пароль
                Smtp.Credentials = new NetworkCredential(MailSender.Email, MailSender.Password);
                Smtp.EnableSsl = true;

                //для каждой строчки файла с адресами отправляем сообщение
                foreach (AppUser i in sendMessage.ArrayTo)
                {
                    //кому отправляем
                    MailAddress to = new MailAddress(i.Email);

                    //очищаем коллекцию адресов получателей, иначе он покажет весь список получателей в каждом сообщении
                    mailMessage.To.Clear();

                    //добавляем элемент в коллекцию адресов получателей
                    mailMessage.To.Add(to);

                    //отправка сообщения
                    Smtp.Send(mailMessage);

                    Console.WriteLine(to);
                }
            }
            Console.WriteLine("Готово");
        }
    }
}
