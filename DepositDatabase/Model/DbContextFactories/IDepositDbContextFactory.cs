namespace DepositDatabase.Model
{
    public interface IDepositDbContextFactory
    {
        DepositDbContext CreateDbContext();
    }
}