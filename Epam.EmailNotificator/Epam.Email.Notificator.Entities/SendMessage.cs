using System.Collections.Generic;

namespace EmailNotificator
{
    public class SendMessage
    {
        public AppUser[] ArrayTo { get; set; }
        public AppUser[] ArrayCopy { get; set; }
        public string Message{get;set; }
        public string Subject { get; set; }
        public List<AttacheFile> AttacheFiles { get; set; }

        public SendMessage (string subject, string message, List<AttacheFile> attacheFiles, AppUser[] arrayTo, AppUser[] arrayCopy)
        {
            Subject = subject;
            Message = message;
            AttacheFiles = attacheFiles;
            ArrayTo = arrayTo;
            ArrayCopy = arrayCopy;
        }

        
    }
}
