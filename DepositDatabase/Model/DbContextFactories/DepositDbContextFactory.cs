namespace DepositDatabase.Model.DbContextFactories
{
    public class DepositDbContextFactory : IDepositDbContextFactory
    {
        public DepositDbContext CreateDbContext()
        {
            return new DepositDbContext();
        }
    }
}
