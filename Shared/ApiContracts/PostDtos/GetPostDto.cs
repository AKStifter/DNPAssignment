using Entities;

namespace ApiContracts.PostDtos;

public class GetPostDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required int UserId { get; set; }
    public required string UserName { get; set; }
    public required IQueryable<Comment> Comments { get; set; }
}