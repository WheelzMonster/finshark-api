using System.ComponentModel.DataAnnotations;

namespace finshark_api.DTOs.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(1)]
    public string Title { get; set; } = null!;
    [Required]
    [MinLength(1)]
    public string Content { get; set; } = null!;
}