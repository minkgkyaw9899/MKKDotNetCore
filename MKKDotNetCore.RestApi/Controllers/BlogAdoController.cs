using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using MKKDotNetCore.RestApi.Models;
using MKKDotNetCore.RestApi.Services;

namespace MKKDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoController : ControllerBase
{
    [HttpGet]
    public IActionResult GatAll()
    {
        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog]";

        connection.Open();

        var cmd = new SqlCommand(query, connection);

        var adaptor = new SqlDataAdapter(cmd);

        var dt = new DataTable();

        adaptor.Fill(dt);

        connection.Close();

        // method 1

        // var blogList = new List<BlogsModel>();

        // foreach (BlogsModel dr in dt.Rows)
        // {
        //     var blog = new BlogsModel()
        //     {
        //         BlogId = Convert.ToInt32(dr.BlogId),
        //         BlogContent = Convert.ToString(dr.BlogContent),
        //         BlogAuthor = Convert.ToString(dr.BlogAuthor),
        //         BlogTitle = Convert.ToString(dr.BlogTitle)
        //     };
        //
        //     blogList.Add(blog);
        // }

        // method 2 

        List<BlogsModel> blogList = dt.AsEnumerable().Select(dr => new BlogsModel()
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        }).ToList();

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

        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        connection.Open();

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogTitle", body.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", body.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", body.BlogContent);

        var result = cmd.ExecuteNonQuery();

        connection.Close();

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

        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        connection.Open();

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", body.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", body.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", body.BlogContent);

        var result = cmd.ExecuteNonQuery();

        connection.Close();

        if (result > 0)
        {
            return Ok("Blog updated successfully");
        }
        else
        {
            return Conflict("Can't update blog");
        }
    }

    [HttpPatch("{id}")]
    public IActionResult PatchUpdate(int id, BlogsModel body)
    {
        var existedBlog = FindBlogById(id);

        if (existedBlog is null) return NotFound("Blog not found");

        const string query =
            @"UPDATE [dbo].[Tbl_Blog] SET [BlogTitle]=ISNULL(@BlogTitle, [BlogTitle]), [BlogAuthor]=ISNULL(@BlogAuthor, [BlogAuthor]), [BlogContent]=ISNULL(@BlogContent, [BlogContent]) WHERE BlogId=@BlogId";

        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        connection.Open();

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogId", id);

        if (body.BlogTitle != null)
        {
            cmd.Parameters.AddWithValue("@BlogTitle", body.BlogTitle);
        }

        if (body.BlogAuthor != null)
        {
            cmd.Parameters.AddWithValue("@BlogAuthor", body.BlogAuthor);
        }

        if (body.BlogContent != null)
        {
            cmd.Parameters.AddWithValue("@BlogContent", body.BlogContent);
        }

        var result = cmd.ExecuteNonQuery();

        connection.Close();

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

        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        connection.Open();

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogId", id);

        var result = cmd.ExecuteNonQuery();

        connection.Close();

        if (result > 0)
        {
            return Ok("Blog deleted successfully");
        }
        else
        {
            return Conflict("Can't delete blog");
        }
    }

    private BlogsModel? FindBlogById(int id)
    {
        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

        var connection = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        connection.Open();
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        var adaptor = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adaptor.Fill(dt);
        connection.Close();

        if (dt.Rows.Count == 0)
        {
            return null;
        }
        else
        {
            var blog = new BlogsModel()
            {
                BlogId = Convert.ToInt32(dt.Rows[0]["BlogId"]),
                BlogAuthor = Convert.ToString(dt.Rows[0]["BlogAuthor"]),
                BlogTitle = Convert.ToString(dt.Rows[0]["BlogTitle"]),
                BlogContent = Convert.ToString(dt.Rows[0]["BlogContent"])
            };

            return blog;
        }
    }
}