namespace ApiContracts.CommentDtos;

public class CreateCommentDto
{
    public required int UserId { get; set; }
    public required string Body { get; set; }
}