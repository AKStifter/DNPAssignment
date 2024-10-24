namespace ApiContracts.CommentDtos;

public class UpdateCommentResponseDto
{
    public required int Id { get; set; }
    
    public required string Body { get; set; }
}