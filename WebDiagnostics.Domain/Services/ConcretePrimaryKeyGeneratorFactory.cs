using System;
using WebDiagnostics.Domain.Interfaces;

namespace WebDiagnostics.Domain.Services
{
    public class ConcretePrimaryKeyGeneratorFactory : PrimaryKeyGeneratorFactory
    {
        public override IPrimaryKeyGenerator GetPrimaryKeyGenerator(Type type)
        {
            //return implementation based on type, 
            //return NULL if no custom generaotr is required
            //return custom if you need a custom one
            //default behaviour is it never returns the Specialised key generator one unless you specify otherwise (99% of the time that's what is used)       
            if (type == typeof (Example))
            {
                return new SpecialisedPrimaryKeyGenerator();
            }

            return null;
        }
    }
}
