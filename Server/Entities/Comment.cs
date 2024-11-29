using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    private Comment()
    {
    }

    public Comment(int userId, int postId, string body)
    {
        UserId = userId;
        PostId = postId;
        Body = body;
    }
    
    public Comment(int id, int userId, int postId, string body)
    {
        Id = Id;
        UserId = userId;
        PostId = postId;
        Body = body;
    }

    public string ToString()
    {
        return $"Id: {Id}, Body: {Body}, UserId: {UserId}, PostId: {PostId}";
    }
}