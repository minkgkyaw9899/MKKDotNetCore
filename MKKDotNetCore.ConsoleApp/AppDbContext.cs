using Microsoft.EntityFrameworkCore;

namespace MKKDotNetCore.ConsoleApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString.StringBuilder.ConnectionString);
    }

    public DbSet<BlogDto> Blogs { get; set; }
};