namespace MKKDotNetCore.ApiWithNLayer.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(new SqlConnection(ConnectionStringBuilder.SqlConnectionString));
    }

    public DbSet<BlogModel> Blog { get; set; }
}