namespace MKKDotNetCore.ApiWithNLayer.Features;

[Route("api/[controller]")]
[ApiController]
public class BlogController: ControllerBase
{
    private readonly BlogBusinessLayer _blogBusinessLayer = new BlogBusinessLayer();

    [HttpGet]
    public IActionResult GetAll()
    {
        var blogs = _blogBusinessLayer.GetAllBlogs();

        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var blog = _blogBusinessLayer.GetBlogById(id);

        if (blog is null)
        {
            return NotFound("Blog not found!");
        }
        
        return Ok(blog);
    }
    
    [HttpPost]
    public IActionResult Create(BlogModel requestModal)
    {
        var result = _blogBusinessLayer.CreateBlog(requestModal);

        if (result == 0)
        {
            return Conflict("Can't create blog");
        }

        return Ok("Successfully created");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel requestModal)
    {
        var result = _blogBusinessLayer.UpdateBlog(id, requestModal);

        if (result == 0)
        {
            return Conflict("Can't update blog");
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _blogBusinessLayer.DeleteBlog(id);

        if (result == 0)
        {
            return Conflict("Can't delete blog");
        }

        return Ok("Successfully deleted");
    }
    
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel requestModal)
    {
        var result = _blogBusinessLayer.UpdateBlog(id, requestModal);

        if (result == 0)
        {
            return Conflict("Can't patch blog");
        }

        return Ok("Successfully patched");
    }
}