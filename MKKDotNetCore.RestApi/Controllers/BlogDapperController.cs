﻿using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MKKDotNetCore.RestApi.Models;
using MKKDotNetCore.RestApi.Services;

namespace MKKDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapperController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll()
    {
        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog]";

        var blogs = db.Query<BlogsModel>(query).ToList();

        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var blog = FindModelById(id);

        if (blog == null)
        {
            return NotFound();
        }

        return Ok(blog);
    }

    [HttpPost]
    public IActionResult Create(BlogsModel body)
    {
        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        const string query = @"
        INSERT INTO 
            [DBO].[Tbl_Blog] 
                ([BlogTitle], [BlogAuthor], [BlogContent]) 
            VALUES 
                (@BlogTitle, @BlogAuthor, @BlogContent)
        ";

        var result = db.Execute(query, body);

        var message = result > 0 ? "Created successfully" : "Create failed";

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateWithPatch(int id, BlogsModel body)
    {

        var blog = FindModelById(id);

        if (blog is null)
        {
            return NotFound("Blog not found");
        }

        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        var conditions = string.Empty;

        if (!string.IsNullOrEmpty(body.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle, ";
        }

        if (!string.IsNullOrEmpty(body.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor, ";
        }

        if (!string.IsNullOrEmpty(body.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent, ";
        }

        if (conditions.Length == 0)
        {
            return NotFound("No fields provided for update");
        }

        conditions += conditions.Substring(0, conditions.Length - 2);

        var query = $@"
        UPDATE
            [DBO].[Tbl_Blog]
        SET
            {conditions}
        WHERE
            BlogId = @BlogId
        ";

        var result = db.Execute(query, body);

        var message = result > 0 ? "Updated successfully" : "Update failed";

        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateWithPut(int id, BlogsModel body)
    {
        if (body is null)
        {
            return BadRequest("Invalid request body");
        }

        var existingBlog = FindModelById(id);

        if (existingBlog is null)
        {
            return NotFound("Blog not found");
        }

        const string query = @"
        UPDATE
            [DBO].[Tbl_Blog]
        SET
            [BlogTitle] = @BlogTitle,
            [BlogAuthor] = @BlogAuthor,
            [BlogContent] = @BlogContent
        WHERE
            BlogId = @BlogId
        ";

        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        var result = db.Execute(query, body);

        var message = result > 0 ? "Updated successfully" : "Update failed";

        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        var blog = FindModelById(id);

        if (blog == null)
        {
            return NotFound();
        }

        const string query = @"
        DELETE FROM
            [DBO].[Tbl_Blog]
        WHERE
            BlogId = @BlogId
        ";

        var result = db.Execute(query, new BlogsModel() { BlogId = id });

        var message = result > 0 ? "Deleted successfully" : "Delete failed";

        return Ok(message);
    }

    private BlogsModel? FindModelById(int id)
    {
        using IDbConnection db = new SqlConnection(SqlConnectionBuilder.Builder.ConnectionString);

        const string query = @"SELECT * FROM [Dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

        return db.Query<BlogsModel>(query, new BlogsModel() { BlogId = id }).FirstOrDefault();
    }
}