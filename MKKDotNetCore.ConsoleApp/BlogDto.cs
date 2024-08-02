namespace MKKDotNetCore.ConsoleApp;

// ols school way
public class BlogDto
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
}

// new school way
// public record BlogEntity(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);