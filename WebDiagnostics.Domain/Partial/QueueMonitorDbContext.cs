using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using WebDiagnostics.Domain.Attributes;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.Domain.Services;

namespace WebDiagnostics.Domain
{
    //public class QueueMonitorDbContext : QueueMonitorConnection, IContext
    public class QueueMonitorDbContext : QueueMonitorDbEntities, IContext
    {
        private readonly PrimaryKeyGeneratorFactory _keyGeneratorFactory;
        public QueueMonitorDbContext(PrimaryKeyGeneratorFactory keyGeneratorFactory)
        {
            Configuration.ProxyCreationEnabled = false;
            _keyGeneratorFactory = keyGeneratorFactory;
        }

        public QueueMonitorDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public override int SaveChanges()
        {

            //check to see if we need to run custom PK generation procedure for any added entities
            var addedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added);

            foreach (DbEntityEntry entry in addedEntities)
            {
                
                var keyName = entry.Entity.GetType().GetCustomAttributes(typeof(SpecialisedKeyAttribute), true)
                    .Select(x => ((SpecialisedKeyAttribute) x).KeyName).DefaultIfEmpty().First();

                var tableName = entry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true)
                    .Select(x => ((TableAttribute) x).Name).DefaultIfEmpty().First();


                var keyGenerator = _keyGeneratorFactory.GetPrimaryKeyGenerator(entry.Entity.GetType());
                if (keyGenerator != null)
                {
                    var newKeyValue = keyGenerator.GeneratePrimaryKey(tableName, keyName);
                    //set the value on the key property
                    entry.Property(keyName).CurrentValue = newKeyValue;
                }              
                
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// needs to work in conjunction with ValidateBeforeSave method
        /// Not implemented at this moment
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            var result = SaveChanges();
            return "";
        }

        public System.Collections.Generic.IEnumerable<T> ManagedEntities<T>()
        {
            var items = ChangeTracker.Entries();
            return items.Where(entry => entry.Entity is T)
                .Select(entry => (T)entry.Entity);

        }

        /// <summary>
        /// still to be done
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <returns></returns>
        public bool ValidateBeforeSave(out string validationErrors)
        {
            throw new System.NotImplementedException();
        }
    }
}



