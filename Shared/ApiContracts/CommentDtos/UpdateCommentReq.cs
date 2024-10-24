namespace ApiContracts.CommentDtos;

public class UpdateCommentReq
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int PostId { get; set; }
    public required string Body { get; set; }
}