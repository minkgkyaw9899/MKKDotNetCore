using System.Diagnostics.CodeAnalysis;

namespace MKKDotNetCore.ApiWithNLayer.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    [Key] public int BlogId { get; set; }

    [AllowNull] public string BlogTitle { get; set; }

    [AllowNull] public string BlogAuthor { get; set; }

    [AllowNull] public string BlogContent { get; set; }
}