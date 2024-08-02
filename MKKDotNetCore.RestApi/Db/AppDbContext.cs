using Microsoft.EntityFrameworkCore;
using MKKDotNetCore.RestApi.Models;
using MKKDotNetCore.RestApi.Services;

namespace MKKDotNetCore.RestApi.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(SqlConnectionBuilder.Builder.ConnectionString);
    }

    public DbSet<BlogsModel> BlogModels { get; set; }
}