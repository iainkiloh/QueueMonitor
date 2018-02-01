using System;

namespace WebDiagnostics.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SpecialisedKeyAttribute : Attribute
    {
        public string KeyName { get; set; }
    }
}
