using Microsoft.EntityFrameworkCore;
using MKKDotNetCoreMvcApp.Models;

namespace MKKDotNetCoreMvcApp.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}