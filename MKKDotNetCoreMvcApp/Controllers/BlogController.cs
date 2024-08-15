using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKKDotNetCoreMvcApp.Db;

namespace MKKDotNetCoreMvcApp.Controllers;

public class BlogController: Controller
{
    private readonly  AppDbContext _db;
    
    public BlogController(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> Index()
    {
        var blogs = await _db.Blogs.AsNoTracking().ToListAsync();
        
        return View(blogs);
    }
}