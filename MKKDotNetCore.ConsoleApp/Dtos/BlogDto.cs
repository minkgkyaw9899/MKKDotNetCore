using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MKKDotNetCore.ConsoleApp.Dtos;

// ols school way

[Table("Tbl_Blog")]
public class BlogDto
{
    [Key]
    public int BlogId { get; set; }
    
    [AllowNull]
    public string BlogTitle { get; set; }
    
    [AllowNull]
    public string BlogAuthor { get; set; }
    
    [AllowNull]
    public string BlogContent { get; set; }
}

// new school way
// public record BlogDto(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);