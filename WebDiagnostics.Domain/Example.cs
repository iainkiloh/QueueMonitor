//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDiagnostics.Domain
{
    using System;
    using System.Collections.Generic;
    
    using WebDiagnostics.Domain.Attributes;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [SpecialisedKey(KeyName = "ExampleId")]
    [Table("Example")]
    public partial class Example
    {
        public string ExampleId { get; set; }
        public string ExampleDesc { get; set; }
    }
}
