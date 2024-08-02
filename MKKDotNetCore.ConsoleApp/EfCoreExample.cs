namespace MKKDotNetCore.ConsoleApp;

public class EfCoreExample
{
    public void Run()
    {
        Read();
        Edit(17);
        Create("New title", "New Author", "New Content");
        Update(7, "New title 2", "New Author 2", "New Content 2");
        Delete(3);
    }

    private void Read()
    {
        var db = new AppDbContext();

        var blogs = db.Blogs.ToList();

        foreach (BlogDto blog in blogs)
        {
            Console.WriteLine("id ==> " + blog.BlogId);
            Console.WriteLine("title ==> " + blog.BlogTitle);
            Console.WriteLine("author ==> " + blog.BlogAuthor);
            Console.WriteLine("content ==> " + blog.BlogContent);
            Console.WriteLine("===================================");
        }
    }

    private void Edit(int id)
    {
        var db = new AppDbContext();

        var blog = db.Blogs.FirstOrDefault(item => item.BlogId == id);

        if (blog is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }
        
        Console.WriteLine("id ==> " + blog.BlogId);
        Console.WriteLine("title ==> " + blog.BlogTitle);
        Console.WriteLine("author ==> " + blog.BlogAuthor);
        Console.WriteLine("content ==> " + blog.BlogContent);
        Console.WriteLine("===================================");
    }

    private void Create(string title, string author, string content)
    {
        var db = new AppDbContext();

        var item = new BlogDto()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };
        
        db.Blogs.Add(item);

        var result = db.SaveChanges();
        
        var message = result > 0 ? "Save Success" : "Save Fail";
        
        Console.WriteLine(message);
    }

    private void Update(int id, string title, string author, string content)
    {
        var db = new AppDbContext();

        var blog = db.Blogs.FirstOrDefault(item => item.BlogId == id);

        if (blog is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        blog.BlogTitle = title;
        blog.BlogAuthor = author;
        blog.BlogContent = content;

        var result = db.SaveChanges();

        var message = result > 0 ? "Update Success" : "Update Fail";

        Console.WriteLine(message);
    }

    private void Delete(int id)
    {
        var db = new AppDbContext();

        var blog = db.Blogs.FirstOrDefault(item => item.BlogId == id);
        
        if(blog is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        db.Blogs.Remove(blog);
        
        var result = db.SaveChanges();
        
        var message = result > 0 ? "Delete Success" : "Delete Fail";
        
        Console.WriteLine(message);
    }
}