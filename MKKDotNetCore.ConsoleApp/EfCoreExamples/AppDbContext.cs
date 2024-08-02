using Microsoft.EntityFrameworkCore;
using MKKDotNetCore.ConsoleApp.Dtos;
using MKKDotNetCore.ConsoleApp.Services;

namespace MKKDotNetCore.ConsoleApp.EfCoreExamples;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString.StringBuilder.ConnectionString);
    }

    public DbSet<BlogDto> Blogs { get; set; }
};