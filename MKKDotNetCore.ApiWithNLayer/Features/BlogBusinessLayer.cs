namespace MKKDotNetCore.ApiWithNLayer.Features;

public class BlogBusinessLayer
{
    private readonly BlogDataLayer _dataLayerBlog = new BlogDataLayer();

    public List<BlogModel> GetAllBlogs()
    {
        var result = _dataLayerBlog.GetAllBlogs();
        return result;
    }

    public BlogModel? GetBlogById(int id)
    {
        var result = _dataLayerBlog.GetBlogById(id);
        return result;
    }

    public int CreateBlog(BlogModel newBlog)
    {
        var result = _dataLayerBlog.CreateBlog(newBlog);
        return result;
    }

    public int UpdateBlog(int id, BlogModel updatedBlog)
    {
        var result = _dataLayerBlog.UpdateBlog(id, updatedBlog);
        return result;
    }

    public int DeleteBlog(int id)
    {
        var result = _dataLayerBlog.DeleteBlog(id);
        return result;
    }
}