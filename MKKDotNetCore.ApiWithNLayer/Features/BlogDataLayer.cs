namespace MKKDotNetCore.ApiWithNLayer.Features;

public class BlogDataLayer
{
    private readonly AppDbContext _appDbContext = new AppDbContext();

    public BlogDataLayer()
    {
    }

    public List<BlogModel> GetAllBlogs()
    {
        var blogs = _appDbContext.Blog.ToList();
        return blogs;
    }

    public BlogModel? GetBlogById(int id)
    {
        var blog = _appDbContext.Blog.FirstOrDefault(x => x.BlogId == id);
        return blog;
    }

    public int CreateBlog(BlogModel requestModal)
    {
        _appDbContext.Blog.Add(requestModal);

        var result = _appDbContext.SaveChanges();
        return result;
    }

    public int UpdateBlog(int id, BlogModel requestModal)
    {
        var blog = _appDbContext.Blog.FirstOrDefault(x => x.BlogId == id);

        if (blog is null) return 0;

        blog.BlogContent = requestModal.BlogContent;
        blog.BlogTitle = requestModal.BlogTitle;
        blog.BlogAuthor = requestModal.BlogAuthor;

        var result = _appDbContext.SaveChanges();
        return result;
    }

    public int DeleteBlog(int id)
    {
        var blog = _appDbContext.Blog.FirstOrDefault(x => x.BlogId == id);

        if (blog is null) return 0;

        _appDbContext.Blog.Remove(blog);

        var result = _appDbContext.SaveChanges();
        return result;
    }
}