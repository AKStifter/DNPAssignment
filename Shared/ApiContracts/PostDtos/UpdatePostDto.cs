namespace ApiContracts.PostDtos;

public class UpdatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required int UserId { get; set; }
}