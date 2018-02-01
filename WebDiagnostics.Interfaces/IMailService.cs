namespace WebDiagnostics.Interfaces
{
    public interface IMailService
    {
        bool Send(IEmailMessage message);

    }
}
