namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Comment(int userId, int postId, string body)
    {
        UserId = userId;
        PostId = postId;
        Body = body;
    }

    public string ToString()
    {
        return $"Id: {Id}, Body: {Body}, UserId: {UserId}, PostId: {PostId}";
    }
}