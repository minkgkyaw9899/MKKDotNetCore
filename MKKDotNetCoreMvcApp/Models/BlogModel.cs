using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MKKDotNetCoreMvcApp.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    [Key] public int Id { get; set; }

    [MaxLength(255)] public string BlogTitle { get; set; } = null;

    [MaxLength(255)] public string BlogAuthor { get; set; } = "";

    [MaxLength(1000)] public string BlogContent { get; set; } = "";
}