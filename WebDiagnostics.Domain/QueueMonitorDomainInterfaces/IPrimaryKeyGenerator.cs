namespace WebDiagnostics.Domain.Interfaces
{
    public interface IPrimaryKeyGenerator
    {
        string GeneratePrimaryKey(string tablename, string keyName);
    }
}
