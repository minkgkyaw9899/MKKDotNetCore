using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MKKDotNetCore.RestApi.Models;

[Table("Tbl_Blog")]
public class BlogsModel
{
    [Key] public int BlogId { get; set; }

    [MaxLength(100)]
    public string? BlogTitle { get; set; }

    [MaxLength(100)]
    public string? BlogAuthor { get; set; }

    [MaxLength(256)]
    public string? BlogContent { get; set; }
}

// public readonly record  struct BlogModel(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);