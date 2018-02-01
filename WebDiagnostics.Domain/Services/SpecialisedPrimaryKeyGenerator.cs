using System;
using System.Data.Entity.Core.Objects;
using WebDiagnostics.Domain.Interfaces;

namespace WebDiagnostics.Domain.Services
{
    public class SpecialisedPrimaryKeyGenerator : IPrimaryKeyGenerator
    {

        private QueueMonitorDbEntities _context;

        public string GeneratePrimaryKey(string tablename, string keyName)
        {          
            
            var sIDParameter = new ObjectParameter("sID", typeof(string));

            using (_context = new QueueMonitorDbEntities())
            {
                //call the procedure which does pk generation
               _context.GenerateSpecialisedPK(tablename, sIDParameter);       
            }

            return sIDParameter.Value.ToString();

        }
    }
}
