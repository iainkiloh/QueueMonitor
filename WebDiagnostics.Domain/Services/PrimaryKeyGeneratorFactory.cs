using System;
using WebDiagnostics.Domain.Interfaces;

namespace WebDiagnostics.Domain.Services
{
    public abstract class PrimaryKeyGeneratorFactory
    {
        public abstract IPrimaryKeyGenerator GetPrimaryKeyGenerator(Type type);
    }
}
