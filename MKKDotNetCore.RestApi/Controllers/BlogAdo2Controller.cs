namespace MKKDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdo2Controller : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService =
        new AdoDotNetService(SqlConnectionBuilder.Builder.ConnectionString);

    [HttpGet]
    public IActionResult GatAll()
    {
        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog]";

        var blogList = _adoDotNetService.Query<BlogsModel>(query);

        if (blogList is null)
        {
            return NotFound("Blogs not found!");
        }

        return Ok(blogList);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var blog = FindBlogById(id);

        if (blog is null) return NotFound("Blog not found");

        return Ok(blog);
    }

    [HttpPost]
    public IActionResult Create(BlogsModel body)
    {
        const string query =
            @"INSERT INTO [dbo].[Tbl_Blog] ([BlogTitle], [BlogAuthor], [BlogContent]) VALUES (@BlogTitle, @BlogAuthor, @BlogContent)";

        var paramList = new List<AdoDotNetParameter>();

        if (body.BlogTitle is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogTitle", body.BlogTitle));
        }

        if (body.BlogContent is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogContent", body.BlogContent));
        }

        if (body.BlogAuthor is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogAuthor", body.BlogAuthor));
        }

        var result = _adoDotNetService.Execute(query, paramList.ToArray());

        if (result > 0)
        {
            return Ok("Blog created successfully");
        }
        else
        {
            return Conflict("Can't create new blog");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogsModel body)
    {
        if (body.BlogAuthor is null || body.BlogTitle is null || body.BlogContent is null)
        {
            return BadRequest("Blog data is required");
        }

        var existedBlog = FindBlogById(id);

        if (existedBlog is null) return NotFound("Blog not found");

        const string query =
            @"UPDATE [dbo].[Tbl_Blog] SET [BlogTitle]=@BlogTitle, [BlogAuthor]=@BlogAuthor, [BlogContent]=@BlogContent WHERE BlogId=@BlogId";

        var paramList = new List<AdoDotNetParameter>()
        {
            new AdoDotNetParameter("@BlogId", id)
        };

        if (body.BlogTitle is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogTitle", body.BlogTitle));
        }

        if (body.BlogContent is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogContent", body.BlogContent));
        }

        if (body.BlogAuthor is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogAuthor", body.BlogAuthor));
        }

        var result = _adoDotNetService.Execute(query, paramList.ToArray());

        if (result > 0)
        {
            return Ok("Blog updated successfully");
        }

        return Conflict("Can't update blog");
    }

    [HttpPatch("{id}")]
    public IActionResult PatchUpdate(int id, BlogsModel body)
    {
        var existedBlog = FindBlogById(id);

        if (existedBlog is null) return NotFound("Blog not found");

        const string query =
            @"UPDATE [dbo].[Tbl_Blog] SET [BlogTitle]=ISNULL(@BlogTitle, [BlogTitle]), [BlogAuthor]=ISNULL(@BlogAuthor, [BlogAuthor]), [BlogContent]=ISNULL(@BlogContent, [BlogContent]) WHERE BlogId=@BlogId";

        var paramList = new List<AdoDotNetParameter>()
        {
            new AdoDotNetParameter("@BlogId", id)
        };

        if (body.BlogTitle is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogTitle", body.BlogTitle));
        }

        if (body.BlogContent is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogContent", body.BlogContent));
        }

        if (body.BlogAuthor is not null)
        {
            paramList.Add(new AdoDotNetParameter("@BlogAuthor", body.BlogAuthor));
        }

        var result = _adoDotNetService.Execute(query, paramList.ToArray());

        if (result > 0)
        {
            return Ok("Blog updated successfully");
        }
        else
        {
            return Conflict("Can't update blog");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existedBlog = FindBlogById(id);

        if (existedBlog is null) return NotFound("Blog not found");

        const string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

        var result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

        if (result > 0)
        {
            return Ok("Blog deleted successfully");
        }

        return Conflict("Can't delete blog");
    }

    private BlogsModel? FindBlogById(int id)
    {
        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

        var blog = _adoDotNetService.QueryOne<BlogsModel>(query, new AdoDotNetParameter("@BlogId", id));

        return blog;
    }
}