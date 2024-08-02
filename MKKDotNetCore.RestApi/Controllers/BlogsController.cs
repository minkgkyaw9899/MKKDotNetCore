using Microsoft.AspNetCore.Mvc;
using MKKDotNetCore.RestApi.Db;
using MKKDotNetCore.RestApi.Models;

namespace MKKDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly AppDbContext _dbContext = new AppDbContext();

    // GET All
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var blogs = _dbContext.BlogModels.ToList();

            return Ok(blogs);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at GetAllBlog ==> " + e);
            throw;
        }
    }

    // CREATE
    [HttpPost]
    public IActionResult Create(BlogsModel blog)
    {
        try
        {
            _dbContext.Add(blog);
            var result = _dbContext.SaveChanges();

            var message = result > 0 ? "Successfully created blog" : "Failed to create blog";

            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at create blog ==> " + e);
            throw;
        }
    }

    // GET
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var blog = _dbContext.BlogModels.FirstOrDefault(item => item.BlogId == id);

            if (blog == null)
            {
                return NotFound("No blog found!");
            }

            return Ok(blog);
        }
        catch (Exception e)
        {
            Console.WriteLine("error at GetById ==> " + e);
            throw;
        }
    }

    // PATCH
    [HttpPatch("{id}")]
    public IActionResult Update(int id, BlogsModel blog)
    {
        try
        {
            var existingBlog = _dbContext.BlogModels.FirstOrDefault(item => item.BlogId == id);

            if (existingBlog is null)
            {
                return NotFound("No blog found!");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                existingBlog.BlogTitle = blog.BlogTitle;
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                existingBlog.BlogContent = blog.BlogContent;
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                existingBlog.BlogAuthor = blog.BlogAuthor;
            }


            var result = _dbContext.SaveChanges();

            var message = result > 0 ? "Successfully updated blog" : "Failed to update blog";

            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at create blog ==> " + e);
            throw;
        }
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Put(int id, BlogsModel body)
    {
        try
        {
            var existingBlog = _dbContext.BlogModels.FirstOrDefault(item => item.BlogId == id);

            if (existingBlog is null)
            {
                return NotFound("No blog found!");
            }

            existingBlog.BlogContent = body.BlogContent;
            existingBlog.BlogTitle = body.BlogTitle;
            existingBlog.BlogAuthor = body.BlogAuthor;

            var result = _dbContext.SaveChanges();

            var message = result > 0 ? "Successfully updated blog" : "Failed to update blog";

            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at put blog ==> " + e);
            throw;
        }
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var blog = _dbContext.BlogModels.FirstOrDefault(item => item.BlogId == id);

            if (blog is null)
            {
                return NotFound("No blog found!");
            }
            
            _dbContext.Remove(blog);
            
            var result = _dbContext.SaveChanges();
            
            var message = result > 0 ? "Successfully deleted blog" : "Failed to delete blog";
            
            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at delete blog ==> " + e);
            throw;
        }
    }
}