using System.Collections.Generic;

namespace WebDiagnostics.Interfaces
{
    public interface IEmailMessage
    {
        List<string> Recipients { get; set; }
        List<string> CcRecipients { get; set; }
        string UserEmail { get; set; }
        string Subject { get; set; }
        string BodyType { get; set; }
        string EmailBody { get; set; }
        bool CcUser { get; set; }

    }
}
