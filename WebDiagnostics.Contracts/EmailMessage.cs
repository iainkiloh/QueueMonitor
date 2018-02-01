using System.Collections.Generic;

namespace WebDiagnostics.Contracts
{
    public class EmailMessage
    {
        public List<string> Recipients { get; set; }
        public List<string> CcRecipients { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string BodyType { get; set; }
        public string EmailBody { get; set; }
        public bool CcUser { get; set; }
    }
}
