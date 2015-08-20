namespace DomainLogic.Model
{
    public interface IDbEntityService<out TEntity, in TId>
    {
        TEntity GetById(TId id);
    }
}
