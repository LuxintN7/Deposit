using System.Collections.Generic;

namespace DomainLogic.Model
{
    public interface IDbEntityService<TEntity, in TId>
    {
        TEntity GetById(TId id);
        List<TEntity> GetList();
    }
}
