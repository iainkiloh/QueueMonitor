
using System.Collections.Generic;
using System.Data.Entity;
namespace WebDiagnostics.Domain.Interfaces
{
public interface IContext
{
 
 		IDbSet<Example> Examples { get; }
		IDbSet<TaskMessage> TaskMessages { get; }
		IDbSet<User> Users { get; }
     
        string Save();
        IEnumerable<T> ManagedEntities<T>();
        bool ValidateBeforeSave(out string validationErrors);
}
}


