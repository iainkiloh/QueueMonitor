using System.Collections.Generic;

namespace WebDiagnostics.Domain.Interfaces
{
    public interface IEntityRepository<TEntity>
    {
        TEntity GetById(object id);
        TEntity Save(TEntity entity);
        bool Delete(TEntity entity);
        IEnumerable<TEntity> All();
    }
}
