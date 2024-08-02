using System.Data;
using System.Data.SqlClient;
using Dapper;
using MKKDotNetCore.ConsoleApp.Dtos;
using MKKDotNetCore.ConsoleApp.Services;

namespace MKKDotNetCore.ConsoleApp.DapperExamples;

public class DapperExample
{
    // connection 

    public void Run()
    {
        // Read();
        // Edit(14);
        // Edit(20);
        // Create("C#", "Min Kaung Kyaw", "C# is developed by Miscrosoft");
        // Update(16, "Bun", "MKK", "Bun is faster runtime of Javascript");
        // Delete(16);
    }

    private static void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionString.StringBuilder.ConnectionString);

        var list = db.Query<BlogDto>("SELECT * FROM tbl_blog").ToList();

        foreach (var blog in list)
        {
            Console.WriteLine("id --> " + blog.BlogId);
            Console.WriteLine("title --> " + blog.BlogTitle);
            Console.WriteLine("author --> " + blog.BlogAuthor);
            Console.WriteLine("content --> " + blog.BlogContent);
            Console.WriteLine("------------------");
        }
    }
    
    private static void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.StringBuilder.ConnectionString);

        var item = db.Query<BlogDto>("SELECT * FROM tbl_blog WHERE BlogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();

        if (item is null)
        {
            Console.WriteLine("No data found");
            return;
        }

        Console.WriteLine("id --> " + item.BlogId);
        Console.WriteLine("title --> " + item.BlogTitle);
        Console.WriteLine("author --> " + item.BlogAuthor);
        Console.WriteLine("content --> " + item.BlogContent);
        Console.WriteLine("------------------");
    }

    private static void Create(string title, string author, string content)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.StringBuilder.ConnectionString);

        var item = new BlogDto()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        const string query = "INSERT INTO tbl_blog (BlogTitle, BlogAuthor, BlogContent) VALUES (@BlogTitle, @BlogAuthor, @BlogContent)";
        
        var result = db.Execute(query, item);
        
        var msg = result > 0 ? "Save Success" : "Save Fail";
        
        Console.WriteLine(msg);
    }
    
    private static void Update(int id, string title, string author, string content)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.StringBuilder.ConnectionString);

        var item = new BlogDto()
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        const string query = "UPDATE tbl_blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent WHERE BlogId = @BlogId";

        var result = db.Execute(query, item);

        var msg = result > 0 ? "Update Success" : "Update Fail";

        Console.WriteLine(msg);
    }
    
    private static void Delete(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.StringBuilder.ConnectionString);

        var item = new BlogDto()
        {
            BlogId = id,
        };

        const string query = "DELETE FROM tbl_blog WHERE BlogId = @BlogId";

        var result = db.Execute(query, item);

        var msg = result > 0 ? "Delete Success" : "Delete Fail";

        Console.WriteLine(msg);
    }
}